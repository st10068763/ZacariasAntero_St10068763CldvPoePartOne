using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CldvPoePartOne
{
    public partial class MyWorkPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private void LoadProducts()
        {
            string connectionString = "Data Source=sqlserverkhumaloscrafs.database.windows.net;Initial Catalog=khumaloCraftsDB;Persist Security Info=True;User ID=st10068763Zacarias;Password=MyVC@007;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Product_ID, Product_Name, Product_Description, Price, Stock, Product_Image FROM Products";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if the reader has any rows
                            if (reader.HasRows)
                            {
                                // Bind the data to the repeater
                                ProductRepeater.DataSource = reader;
                                ProductRepeater.DataBind();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex.Message);                    
                }
               
                
            }

        }
        protected void BuyButton_Click(object sender, EventArgs e)
        {
            // Get the button that was clicked
            Button buyButton = (Button)sender;

            // Get the product ID from the button's command argument
            int productId = int.Parse(buyButton.CommandArgument);
            // Add the product to the cart
            AddToCart(productId);
        }

        private void AddToCart(int productId)
        {
            // Get the user ID from the session
            int userId = (int)Session["UserId"];

            // Connect to the database
            string connectionString = "Data Source=sqlserverkhumaloscrafs.database.windows.net;Initial Catalog=khumaloCraftsDB;Persist Security Info=True;User ID=st10068763Zacarias;Password=MyVC@007;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Check if the product is already in the cart
                    string checkQuery = "SELECT * FROM Cart WHERE User_ID = @userId AND Product_ID = @productId";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@userId", userId);
                        checkCommand.Parameters.AddWithValue("@productId", productId);
                        using (SqlDataReader reader = checkCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // The product is already in the cart                                
                                ShowError("Product already in cart.");
                                return;                               
                            }
                        }
                    }// end of product check

                    // Add the product to the cart
                    string query = "INSERT INTO Transactions (User_ID, Product_ID, Quantity, transaction_date) VALUES (@userId, @productId, @Quantity, @transaction_date)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.Parameters.AddWithValue("@Quantity", 1);
                        command.Parameters.AddWithValue("@transaction_date", DateTime.Now);
                        // Execute the query to inset the data in the database
                        command.ExecuteNonQuery();
                    }
                    // Message to confirm that the product has been added to the cart
                    ShowError("Product added to cart successfully.");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Method to insert new product into the database
        protected void AddProduct_Click(object sender, EventArgs e)
        {
            // Get the values from the form
            string productName = ProductName.Text;
            string productDescription = ProductDescription.Text;
            decimal price = decimal.Parse(Price.Text);
            int stock = int.Parse(Stock.Text);
           
            // Insert the product into the database
            InsertProduct(productName, productDescription, price, stock);
        }

        private void InsertProduct(string productName, string productDescription, decimal price, int stock)
        {
            // Connect to the database
            string connectionString = "Data Source=sqlserverkhumaloscrafs.database.windows.net;Initial Catalog=khumaloCraftsDB;Persist Security Info=True;User ID=st10068763Zacarias;Password=MyVC@007;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Insert the new product into the database
                    string query = "INSERT INTO Products (Product_Name, Product_Description, Price, Stock) VALUES (@productName, @productDescription, @price, @stock)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@productName", productName);
                        command.Parameters.AddWithValue("@productDescription", productDescription);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@stock", stock);
                        // Execute the query to insert the new product
                        command.ExecuteNonQuery();
                    }
                    // Message to confirm that the product has been added
                    ShowError("Product added successfully.");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ShowError(string message)
        {
            // Display the error message
           ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", "alert('" + message + "');", true);
        }
    }
}