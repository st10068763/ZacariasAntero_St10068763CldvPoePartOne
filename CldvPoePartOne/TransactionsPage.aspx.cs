using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CldvPoePartOne
{
    public partial class TransactionsPage : System.Web.UI.Page
    {
        public class Product
        {
            public int ProductID { get; set; }
            public string productName { get; set; }
            public string productDescription { get; set; }
            public decimal price { get; set; }
            public int stock { get; set; }
            public string productAuthor { get; set; }
            public string productImage { get; set; }
        }

        // gets the user id from the session
        public int UserId => (int)Session["UserId"];

        public int Quantity = 1;
        public decimal TotalPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productId = Request.QueryString["ProductId"];
                if (!string.IsNullOrEmpty(productId))
                {
                    LoadProductDetails(productId);
                }
                else
                {
                    ShowError("No product selected.");
                }
                GetDataForRepeater();
            }
        }

        private List<Product> GetDataForRepeater()
        {
            List<Product> products = new List<Product>();
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Transactions";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ProductID = (int)reader["ProductID"],
                                    productName = reader["productName"].ToString(),
                                    productDescription = reader["productDescription"].ToString(),
                                    price = (decimal)reader["price"],
                                    stock = (int)reader["stock"],
                                    productAuthor = reader["productAuthor"].ToString(),
                                    productImage = reader["productImage"].ToString()
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Error fetching transactions: " + ex.Message);
                }
            }
            return products;
        }

        private void LoadProductDetails(string productId)
        {
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProductID, productName, productDescription, price, stock, productImage, productAuthor FROM ProductTB WHERE ProductID = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Product product = new Product
                                {
                                    ProductID = (int)reader["ProductID"],
                                    productName = reader["productName"].ToString(),
                                    productDescription = reader["productDescription"].ToString(),
                                    price = (decimal)reader["price"],
                                    stock = (int)reader["stock"],
                                    productAuthor = reader["productAuthor"].ToString(),
                                    productImage = reader["productImage"].ToString()
                                };
                                // Store product price in session
                                Session["ProductPrice"] = product.price;
                                Session["ProductStock"] = product.stock;
                                ProductRepeater.DataSource = new List<Product> { product };
                                ProductRepeater.DataBind();
                            }
                            else
                            {
                                ShowError("Product not found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Error fetching product details: " + ex.Message);
                }
            }
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    RepeaterItem item = button.NamingContainer as RepeaterItem;
                    if (item != null)
                    {
                        TextBox quantityTextBox = (TextBox)item.FindControl("QuantityTB");
                        int quantity = int.Parse(quantityTextBox.Text);

                        decimal productPrice = (decimal)Session["ProductPrice"];
                        decimal totalPrice = productPrice * quantity;
                        int stock = (int)Session["ProductStock"];

                        if (quantity > stock)
                        {
                            ErrorMessageLabel.Text = "Quantity selected exceeds available stock.";
                            return;
                        }

                        TextBox shippingAddressTB = (TextBox)item.FindControl("shippingAddressTB");
                        DropDownList paymentMethodDDL = (DropDownList)item.FindControl("PaymentMethodDDL");
                        TextBox cardNumberTB = (TextBox)item.FindControl("CardNumberTB");
                        TextBox expiryDateTB = (TextBox)item.FindControl("ExpiryDateTB");
                        TextBox cvvTB = (TextBox)item.FindControl("CVVTB");
                        // gets the user email address
                        TextBox PaymentReceiptTB = (TextBox)item.FindControl("PaymentReceiptTB"); 

                        if (ProcessPayment(totalPrice, shippingAddressTB.Text, cardNumberTB.Text, expiryDateTB.Text, cvvTB.Text))
                        {
                            string productName = ((Label)item.FindControl("ProductNameLabel"))?.Text ?? "Unknown Product";

                            //string productName = ((Label)item.FindControl("ProductNameLabel")).Text; 
                            string email = PaymentReceiptTB.Text; // Use the email from the form

                            if (ProcessTransaction(UserId, button.CommandArgument, quantity, totalPrice, DateTime.Now, paymentMethodDDL.SelectedValue, shippingAddressTB.Text, "Pending", productName, email))
                            {
                                UpdateProductStock(button.CommandArgument, stock - quantity);
                                SuccessMessageLabel.Text = "Payment successful. Your order will be processed.";
                                // Call the method to send the email
                                SendPaymentConfirmationEmail(productName, quantity, totalPrice, paymentMethodDDL.SelectedValue, cardNumberTB.Text, email);
                            }
                            else
                            {
                                ErrorMessageLabel.Text = "Transaction failed. Please try again.";
                            }
                        }
                        else
                        {
                            ErrorMessageLabel.Text = "Payment failed. Please try again.";
                        }
                    }
                    else
                    {
                        ErrorMessageLabel.Text = "Repeater item not found.";
                    }
                }
                else
                {
                    ErrorMessageLabel.Text = "Button not found.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "Error processing payment: " + ex.Message;
            }
        }



        protected void QuantityTB_TextChanged(object sender, EventArgs e)
        {
            TextBox quantityTextBox = sender as TextBox;
            RepeaterItem item = quantityTextBox.NamingContainer as RepeaterItem;
            Label priceLabel = item.FindControl("TotalPriceLabel") as Label;

            int quantity = int.Parse(quantityTextBox.Text);
            decimal productPrice = (decimal)Session["ProductPrice"];
            decimal totalPrice = productPrice * quantity;

            priceLabel.Text = "R" + totalPrice.ToString("F2");
        }

        private bool ProcessPayment(decimal totalPrice, string shippingAddress, string cardNumber, string expiryDate, string cvv)
        {
            // Example placeholder for payment processing logic
            bool paymentSuccess = true;

            // Actual payment processing logic goes here

            return paymentSuccess;
        }

        private bool ProcessTransaction(int userId, string product_Id, int quantity, decimal totalAmount, DateTime transaction_date, string paymentMethod, string shippingAddress, string transactionStatus, string productName, string email)
        {
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Transactions (UserID, ProductID, Quantity, TotalAmount, TransactionDate, PaymentMethod, ShippingAddress, Status, ProductName, Email) " +
                                   "VALUES (@UserID, @ProductID, @Quantity, @TotalAmount, @TransactionDate, @PaymentMethod, @ShippingAddress, @Status, @ProductName, @Email)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@ProductID", product_Id);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@TransactionDate", transaction_date);
                        cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        cmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
                        cmd.Parameters.AddWithValue("@Status", transactionStatus);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Email", email);

                        int rows = cmd.ExecuteNonQuery();
                        isSuccess = rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error processing transaction: " + ex.Message);
            }
            return isSuccess;
        }

        /// <summary>
        /// Method to update product stock after a successful transaction
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newStock"></param>
        private void UpdateProductStock(string productId, int newStock)
        {
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // update product stock based on the stock user bought
                    string query = "UPDATE ProductTB SET stock = @Stock WHERE ProductID = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Stock", newStock);
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Error updating product stock: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Method to send email to the user
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="quantity"></param>
        /// <param name="totalPrice"></param>
        /// <param name="paymentMethod"></param>
        /// <param name="cardNumber"></param>
        /// <param name="email"></param>
        private void SendPaymentConfirmationEmail(string productName, int quantity, decimal totalPrice, string paymentMethod, string cardNumber, string email)
        {
            try
            {
                string fromEmail = "zarcoticmock@gmail.com"; 
                string fromPassword = "HeyZarcotic01"; 
                string subject = "Payment Confirmation";
                string lastFourDigits = cardNumber.Substring(cardNumber.Length - 4);

                string body = $"<p>Thanks for shopping with us!</p>" +
                              $"<p><strong>Your Transaction Details:</strong></p>" +
                              $"<p>Product Name: {productName}</p>" +
                              $"<p>Quantity: {quantity}</p>" +
                              $"<p>Total Price: R{totalPrice:F2}</p>" +
                              $"<p>Payment Method: {paymentMethod} (**** **** **** {lastFourDigits})</p>";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail);
                    mail.To.Add(email);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("zarcoticmock@gmail.com", "HeyZarcotic01");
                        smtp.EnableSsl = true; 
                        smtp.Send(mail);
                    }

                }

                ShowSuccess("Payment confirmation email sent.");
            }
            catch (Exception ex)
            {
                ShowError("Error sending email: " + ex.Message);
            }
        }


        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }

        private void ShowSuccess(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
    }
}
