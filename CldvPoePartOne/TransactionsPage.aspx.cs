using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.EnterpriseServices;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CldvPoePartOne.TransactionsPage;

namespace CldvPoePartOne
{
    public partial class TransactionsPage : System.Web.UI.Page
    {
        /// <summary>
        /// Product class to hold product details
        /// </summary>
        public class Product
        {
            public int Product_ID { get; set; }
            public string Product_Name { get; set; }
            public string Product_Description { get; set; }
            public float Price { get; set; }
            public int Stock { get; set; }
            public string Author { get; set; }
            public string Image_URL { get; set; }
        }

        // Properties to hold the product details        
        public string ProductStock;
        public string ProductImage;
        public string ProductName;
        public string ProductDescription;
        public float ProductPrice;
        public string ProductAuthor;
        public int Quantity = 1;
        public float TotalPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Update the total price
                UpdateTotalPrice();
                              
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
        //--------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// this will get the data from the database and populate the repeater
        /// </summary>
        /// <returns></returns>
        private List<Product> GetDataForRepeater()
        {
            List<Product> products = new List<Product>();
            string connectionString = "Data Source=sqldatabasekhumalo.database.windows.net;Initial Catalog=khumaloDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";

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
                                // Populate the list of products with data from the database
                                Product product = new Product
                                {
                                    Product_ID = (int)reader["Product_ID"],
                                    Product_Name = reader["Product_Name"].ToString(),
                                    Product_Description = reader["Product_Description"].ToString(),
                                    Price = float.Parse(reader["Price"].ToString()),
                                    Stock = (int)reader["Stock"],
                                    Author = reader["Author"].ToString(),
                                    Image_URL = reader["Image_URL"].ToString()
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
            // Return the populated list
            return products;  
        }

        //--------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to load the product details
        /// </summary>
        /// <param name="productId"></param>
        private void LoadProductDetails(string productId)
        {
            // Check if the product ID is not null or empty, only if the product ID is valid the product details will be fetched
            if (!string.IsNullOrWhiteSpace(productId))
            {
                // connection string
                string connectionString = "Data Source=sqldatabasekhumalo.database.windows.net;Initial Catalog=khumaloDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                // using using statement to ensure the connection is closed after the operation is done
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Open the connection
                        connection.Open();
                        // Query to fetch the product details where the product ID matches the one in the query string
                        string query = "SELECT Product_ID, Product_Name, Product_Description, Price, Stock, Product_Image, Author FROM Products WHERE Product_ID = @ProductId";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ProductId", productId);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    ProductRepeater.DataSource = reader;
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
        }
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        ///  checkout button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckoutButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;
            
            // Get the product ID from the query string
            string productId = Request.QueryString["Product_ID"];
            // Get the user ID from the session
            int userId = (int)Session["UserId"];
            TextBox QuantityInput = (TextBox)item.FindControl("QuantityInput");
            // Get the quantity the user selects to buy
            int quantity = int.Parse(QuantityInput.Text);
            // get the transaction date
            DateTime Transaction_date = DateTime.Now;
            // Load product details
            LoadProductDetails(productId);
            // Calculate the total price for the number of items selected by the user
            float totalPrice = ProductPrice * quantity;


            // Ensure product details are available
            if (!string.IsNullOrEmpty(ProductName) && !string.IsNullOrEmpty(ProductAuthor))
            {
                // Call ProcessTransaction method with correct parameters
                ProcessTransaction(userId, productId, quantity, ProductPrice, Transaction_date, ProductName, ProductAuthor);
            }
            else
            {
                ShowError("Error: Product details are missing.");
            }
        }

       //--------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to process the transaction in the database
        /// </summary>
        /// <param name="transaction_Id"></param>
        /// <param name="user_Id"></param>
        /// <param name="product_Id"></param>
        /// <param name="quantity"></param>
        /// <param name="product_price"></param>
        /// <param name="transaction_date"></param>
        /// <param name="product_name"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        private bool ProcessTransaction( int user_Id, string product_Id, int quantity, float product_price, DateTime transaction_date, string product_name, string author)
        {
            string connectionString = "Data Source=sqldatabasekhumalo.database.windows.net;Initial Catalog=khumaloDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Transactions (User_ID, Product_ID, Quantity, Product_Price, Transaction_date, Product_Name, Author) " +
                                "VALUES (@User_ID, @Product_ID, @Quantity, @Product_Price, @Transaction_date, @Product_Name, @Author)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@User_ID", user_Id);
                        cmd.Parameters.AddWithValue("@Product_ID", product_Id);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Product_Price", product_price);
                        cmd.Parameters.AddWithValue("@Transaction_date", transaction_date);
                        cmd.Parameters.AddWithValue("@Product_Name", product_name);
                        cmd.Parameters.AddWithValue("@Author", author);

                        int rows = cmd.ExecuteNonQuery();
                        isSuccess = rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
            return isSuccess;
        }
        //--------------------------------------------------------------------------------------------------------//
       
        /// <summary>
        /// calculates the total price for the number of items selected by the user
        /// </summary>
        private void UpdateTotalPrice()
        {
            if (ProductRepeater.Items.Count > 0)
            {
                RepeaterItem item = ProductRepeater.Items[0];
                TextBox QuantityInput = (TextBox)item.FindControl("QuantityInput");

                if (int.TryParse(QuantityInput.Text, out int quantity))
                {
                    TotalPrice = ProductPrice * quantity;
                }
                else
                {
                    TotalPrice = ProductPrice;
                }
            }
           
        }      
  

        // Method to show an error message
        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }

        // Method to show a success message
        private void ShowSuccess(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }

        //--------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// this method will handle the item command event of the repeater
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Checkout")
            {
                Button button = e.CommandSource as Button;
                RepeaterItem item = (RepeaterItem)button.NamingContainer;

                int productId = int.Parse(e.CommandArgument.ToString());
                int userId = (int)Session["UserId"];
                TextBox QuantityInput = (TextBox)item.FindControl("QuantityInput");
                int quantity = int.Parse(QuantityInput.Text);
                DateTime transactionDate = DateTime.Now;

                Label PriceLabel = (Label)item.FindControl("TotalPriceLabel");
                float totalPrice = float.Parse(PriceLabel.Text);
               
                if (ProcessTransaction(userId, productId.ToString(), quantity, totalPrice, transactionDate, ProductName, ProductAuthor))
                {
                    ShowSuccess("Transaction successful.");
                }
                else
                {
                    ShowError("Transaction failed.");
                }
            }
        }
    }
}
//--------------------------------------------------***DingDong End Of Code***-------------------------------------------------------------//