using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CldvPoePartOne
{
    public partial class TransactionsPage : System.Web.UI.Page
    {
        // Properties to hold the product details        
        protected string ProductStock;
        protected string ProductImage;
        protected string ProductName;
        protected string ProductDescription;
        protected float ProductPrice;
        protected string ProductAuthor;
        protected int Quantity = 1;
        protected float TotalPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load the product details
                LoadProductDetails();                
                // Update the total price
                UpdateTotalPrice();
            }
        }

        // Method to load the product details
        private void LoadProductDetails()
        {
            // Get the product ID from the query string
            string productId = Request.QueryString["Product_ID"];

            // Get the product details from the database
            string connectionString = "Data Source=sqlserverkhumaloscrafs.database.windows.net;Initial Catalog=khumaloCraftsDB;Persist Security Info=True;User ID=st10068763Zacarias;Password=MyVC@007;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Product_ID, Product_Name, Product_Description, Price, Stock, Image_URL, Author FROM Products WHERE Product_ID = @Product_ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Product_ID", productId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if the reader has any rows
                            if (reader.HasRows)
                            {
                                // Read the product details
                                reader.Read();
                                ProductImage = reader["Image_URL"].ToString();
                                ProductName = reader["Product_Name"].ToString();
                                ProductDescription = reader["Product_Description"].ToString();
                                ProductPrice = float.Parse(reader["Price"].ToString());
                                ProductStock = reader["Stock"].ToString();
                                ProductAuthor = reader["Author"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handles the database exceptions that may occur
                    ShowError("Database error on product details: " + ex.Message);
                }
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
            string connectionString = "Data Source=sqlserverkhumaloscrafs.database.windows.net;Initial Catalog=khumaloCraftsDB;Persist Security Info=True;User ID=st10068763Zacarias;Password=MyVC@007;Encrypt=True;TrustServerCertificate=True";

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