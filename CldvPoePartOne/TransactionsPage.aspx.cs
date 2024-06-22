using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        public float ProductPrice;
        public int Quantity = 1;
        public float TotalPrice;

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
            Button button = sender as Button;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;

            TextBox quantityTextBox = (TextBox)item.FindControl("QuantityTB");
            int quantity = int.Parse(quantityTextBox.Text);

            Label totalPriceLabel = (Label)item.FindControl("TotalPriceLabel");
            float totalPrice = float.Parse(totalPriceLabel.Text.Replace("R", ""));

            TextBox shippingAddressTB = (TextBox)item.FindControl("shippingAddressTB");
            DropDownList paymentMethodDDL = (DropDownList)item.FindControl("PaymentMethodDDL");
            TextBox cardNumberTB = (TextBox)item.FindControl("CardNumberTB");
            TextBox expiryDateTB = (TextBox)item.FindControl("ExpiryDateTB");
            TextBox cvvTB = (TextBox)item.FindControl("CVVTB");

            if (ProcessPayment(totalPrice, shippingAddressTB.Text, cardNumberTB.Text, expiryDateTB.Text, cvvTB.Text))
            {
                if (ProcessTransaction(UserId, button.CommandArgument, quantity, totalPrice, DateTime.Now, paymentMethodDDL.SelectedValue, shippingAddressTB.Text, "Pending", "ProductName", "email@example.com"))
                {
                    ShowSuccess("Payment successful. Your order will be processed.");
                    SendPaymentConfirmationEmail();
                }
                else
                {
                    ShowError("Transaction failed.");
                }
            }
            else
            {
                ShowError("Payment failed. Please try again.");
            }
        }

        protected void QuantityTB_TextChanged(object sender, EventArgs e)
        {
            TextBox quantityTextBox = sender as TextBox;
            RepeaterItem item = quantityTextBox.NamingContainer as RepeaterItem;
            Label priceLabel = item.FindControl("TotalPriceLabel") as Label;

            int quantity = int.Parse(quantityTextBox.Text);
            float pricePerUnit = ProductPrice;
            float totalPrice = pricePerUnit * quantity;

            priceLabel.Text = "R" + totalPrice.ToString("F2");
        }

        private bool ProcessPayment(float totalPrice, string shippingAddress, string cardNumber, string expiryDate, string cvv)
        {
            // Example placeholder for payment processing logic
            bool paymentSuccess = true;

            // Actual payment processing logic goes here

            return paymentSuccess;
        }

        private bool ProcessTransaction(int userId, string product_Id, int quantity, float totalAmount, DateTime transaction_date, string paymentMethod, string shippingAddress, string transactionStatus, string productName, string email)
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

        private void SendPaymentConfirmationEmail()
        {
            // Code to send email to the user with transaction details
            ShowSuccess("Payment confirmation email sent.");
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
