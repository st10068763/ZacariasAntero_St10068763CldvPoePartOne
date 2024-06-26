﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
                // process the products from the database
                LoadProducts();
            }
        }
        //-----------------------------------------------------------------------------------------------//

        /// <summary>
        /// method to search for products in the database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataTable SearchProducts(string query)
        {
            var searchQuery = "%" + query + "%";
            // connection string to connect to the database
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // open the connection to the database
                conn.Open();
                string sqlQuery = "SELECT ProductName, ProductDescription, price, stock, productImage, productAuthor FROM ProductTB WHERE ProductName LIKE @SearchQuery OR productDescription LIKE @SearchQuery OR productAuthor LIKE @SearchQuery";
                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    command.Parameters.AddWithValue("@SearchQuery", searchQuery);
                    // execute the query
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable products = new DataTable();
                        adapter.Fill(products);
                        return products;
                    }
                }
            }
        }

        // Method to load the products from the database
        private void LoadProducts()
        {
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProductID, productName, productDescription, price, stock, productImage, productAuthor FROM ProductTB";
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
                    ShowError("Database error: " + ex.Message);
                }                
            }
        }
        //-------------------------------------ADD TO CART METHOD-------------------------------------//
        /// <summary>
        /// Method to handle the click event of the buy button to add the product to the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void BuyButton_Click(object sender, EventArgs e)
        //{
        //    // Get the button that was clicked
        //   Button buyButton = (Button)sender;
        //    string productId = buyButton.CommandArgument;
        //    Response.Redirect("TransactionsPage.aspx?ProductId=" + productId);
        //}

        protected void BuyButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;
            Response.Redirect($"TransactionsPage.aspx?ProductId={productId}");
        }


        // Method to add the product to the cart
        private void AddToCart(int productId)
        {
            // Get the user ID from the session
            int userId = (int)Session["UserId"];
            // Connect to the database
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Check if the product is already in the cart
                    string checkQuery = "SELECT * FROM Transactions WHERE UserID = @userId AND ProductID = @productId";
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

                    // Add the product to the cart/ to the transactions table
                    string query = "INSERT INTO Transactions (UserID, ProductID, Quantity, transaction_date) VALUES (@userId, @productId, @Quantity, @transaction_date)";
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
                    // redirect to the cart page
                    Response.Redirect($"TransactionsPage.aspx?Product_ID={productId}");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ShowError("Database error: " + ex.Message);
                }
            }
        }
        //------------------------------------INSERT PRODUCT----------------------------------------//
        /// <summary>
        /// method to handle the click event of the add product button to add the product to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string role = (string)Session["UserRole"];
            if (role != "seller")
            {
                ShowError("You do not have permission to add products. Only sellers can add products");
                return;
            }

            string productName = ProductName.Text;
            string productDescription = ProductDescription.Text;
            string productAuthor = ProductAuthorTB.Text;
            if (!float.TryParse(Price.Text, out float price))
            {
                ShowError("Invalid price format.");
                return;
            }
            if (!int.TryParse(Stock.Text, out int stock))
            {
                ShowError("Invalid stock format.");
                return;
            }
            string productImage = ImageURLTB.Text;

            // checks if the product name is empty and not it inserts the product into the database
            if (string.IsNullOrWhiteSpace(productName))
            {
                ShowError("Product name is required.");
            }
            else
            {
                InsertNewProduct(productName, productDescription, price, stock, productAuthor, productImage);
                // clears the fields after the product has been added
                ProductName.Text = "";
                ProductDescription.Text = "";
                Price.Text = "";
                Stock.Text = "";
                ProductAuthorTB.Text = "";
                ImageURLTB.Text = "";

            }
        }

        //===========================================================================================//
        /// <summary>
        ///  Method to insert the product into the database using the values from the form
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productDescription"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <param name="productAuthor"></param>
        /// <param name="productImage"></param>
        private void InsertNewProduct(string productName, string productDescription, float price, int stock, string productAuthor, string productImage)
        {
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO ProductTB (productName, productDescription, price, stock, productAuthor, productImage) VALUES (@productName, @productDescription, @price, @stock, @productAuthor, @productImage)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@productName", productName);
                        command.Parameters.AddWithValue("@productDescription", productDescription);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@stock", stock);
                        command.Parameters.AddWithValue("@productAuthor", productAuthor);
                        command.Parameters.AddWithValue("@productImage", productImage);

                        command.ExecuteNonQuery();
                    }

                    ShowSuccess("Product added successfully.");
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    ShowError($"Database error: {ex.Message}");
                }
            }
        }

        //----------------------------------------------------------//
        // Method to display the error message
        private void ShowError(string message)
        {
            // Display the error message
           ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", "alert('" + message + "');", true);
        }
        // Method to display the success message
        private void ShowSuccess(string message)
        {
            // Display the success message
            ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", "alert('" + message + "');", true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string query = SearchTermTB.Text;
            SearchProducts(query);
            // Perform search and bind results to GridViewProducts
            DataTable products = SearchProducts(query);
            GridViewProducts.DataSource = products;
            GridViewProducts.DataBind();
        }
    }
}