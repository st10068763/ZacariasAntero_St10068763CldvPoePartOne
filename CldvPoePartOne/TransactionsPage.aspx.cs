using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CldvPoePartOne.TransactionsPage;

namespace CldvPoePartOne
{
    public partial class TransactionsPage : System.Web.UI.Page
    {
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
                // Load the product details
                LoadProductDetails();                
                // Update the total price
                UpdateTotalPrice();

                // Retrieve your data source, like from a database or a list
                var myDataSource = GetDataForRepeater();  // This function should return your data source

                // Check if the data source is not empty
                if (myDataSource.Count > 0)
                {
                    TransactionsRepeater.DataSource = myDataSource;  // Assign data source
                    TransactionsRepeater.DataBind();  // Bind data
                }
                else
                {
                    ShowError("No transactions found.");  // Handle empty data case
                }
            }
        }

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
                    ShowError("Error fetching transactions: " + ex.Message);  // Handle error
                }
            }

            return products;  // Return the populated list
        }

        // Method to load the product details
        private void LoadProductDetails()
        {
            // Get the product ID from the page URL
            string productId = Request.QueryString["Product_ID"];
            // Check if the product ID is not null or empty, only if the product ID is valid the product details will be fetched
            if (!string.IsNullOrWhiteSpace(productId))
            {
                // connection string
                string connectionString = "Data Source=sqldatabasekhumalo.database.windows.net;Initial Catalog=khumaloDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
                // using using statement to ensure the connection is closed after the operation is done
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();
                    // Query to fetch the product details where the product ID matches the one in the query string
                    string query = "SELECT * FROM Products WHERE Product_ID = @Product_ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the product ID as a parameter to the query
                        command.Parameters.AddWithValue("@Product_ID", productId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                ProductImage = reader["Product_Image"].ToString();
                                ProductName = reader["Product_Name"].ToString();
                                ProductDescription = reader["Product_Description"].ToString();
                                ProductPrice = float.Parse(reader["Price"].ToString());
                                ProductStock = reader["Stock"].ToString();
                                ProductAuthor = reader["Author"].ToString();
                            }                           
                        }                       
                    }
                }
            }
            else
            {
                // If the product ID is not valid, redirect to the home page
                Response.Redirect("MyWorkPage.aspx");
            }
        }

        

      //----------------------------------------------------------------------------------------------------//
      // Method to display the product details selected by the user in the my work page
      protected void TransactionsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
      {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var product = (Product)e.Item.DataItem;

                var productNameLabel = (Label)e.Item.FindControl("ProductNameLabel");
                if (productNameLabel != null)
                    productNameLabel.Text = product.Product_Name;
                // Find the label control in the repeater
                Label ProductNameLabel = (Label)e.Item.FindControl("ProductNameLabel");
                Label ProductDescriptionLabel = (Label)e.Item.FindControl("ProductDescriptionLabel");
                Label ProductPriceLabel = (Label)e.Item.FindControl("ProductPriceLabel");
                Label ProductStockLabel = (Label)e.Item.FindControl("ProductStockLabel");
                Label ProductAuthorLabel = (Label)e.Item.FindControl("ProductAuthorLabel");
                Image ProductImageLabel = (Image)e.Item.FindControl("ProductImageLabel");

                // Assign the product details to the label controls
                ProductNameLabel.Text = ProductName;
                ProductDescriptionLabel.Text = ProductDescription;
                ProductPriceLabel.Text = ProductPrice.ToString();
                ProductStockLabel.Text = ProductStock;
                ProductAuthorLabel.Text = ProductAuthor;
                ProductImageLabel.ImageUrl = ProductImage;
            }

      }

        protected void CheckoutButton_Click(object sender, EventArgs e)
        {
            // Get the product ID from the query string
            string productId = Request.QueryString["Product_ID"];
            // Get the user ID from the session
            int userId = (int)Session["UserId"];
            // Get the quantity the user selects to buy
            int quantity = int.Parse(QuantityInput.Text);
            // get the transaction date
            DateTime Transaction_date = DateTime.Now;
            // Calculate the total price for the number of items selected by the user
            float totalPrice = ProductPrice * quantity;
           
            // Process the transaction
            ProcessTransaction(userId, int.Parse(productId), quantity, totalPrice, Transaction_date);
            
        }
        /// <summary>
        /// Method to process the transaction
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="totalPrice"></param>
        private void ProcessTransaction(int userId, int productId, int quantity, float totalPrice, DateTime transaction_date)
        {
            // Connect to the database
            string connectionString = "Data Source=sqldatabasekhumalo.database.windows.net;Initial Catalog=khumaloDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Insert the transaction details into the database
                    string query = "INSERT INTO Transactions (User_ID, Product_ID, Quantity, Total_Price, Transaction_date) VALUES (@userId, @productId, @quantity, @totalPrice, @Transaction_date)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@totalPrice", totalPrice);
                        command.Parameters.AddWithValue("@Transaction_date", transaction_date);
                        command.ExecuteNonQuery();
                    }
                    // Message to confirm that the transaction has been processed
                    ShowSuccess("Transaction processed successfully.");
                }
                catch (Exception ex)
                {
                    // Handles the database exceptions that may occur
                    ShowError("Database error on transaction: " + ex.Message);
                }
            }
        }

        // calculates the total price for the number of items selected by the user
        private void UpdateTotalPrice()
        {
            int quantity = 1;  // Default quantity
            if (int.TryParse(QuantityInput.Text, out quantity))  // Ensure valid integer
            {
                TotalPrice = ProductPrice * quantity;  // Calculate the total price
            }
            else
            {
                TotalPrice = ProductPrice;  // Fallback in case of error
            }
        }
       
        // Add the missing method
        protected void TransactionsRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
                // This event handler will be invoked when an item in the repeater is interacted with (like clicking a button)

                // Check the command name to determine the action to take
                if (e.CommandName == "Buy")
                {
                    // Extract the command argument, which could be the Product_ID
                    var productId = e.CommandArgument.ToString();

                    // Redirect to another page or process the item as required
                    Response.Redirect($"ProductDetails.aspx?Product_ID={productId}");
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
    }
}
//--------------------------------------------------***DingDong End Of Code***-------------------------------------------------------------//