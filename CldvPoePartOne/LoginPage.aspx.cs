﻿using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace CldvPoePartOne
{
    public partial class LoginPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       
        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
           
            return inputPassword == storedHash;
        }

        /// <summary>
        /// login button click event handler that authenticates the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void btnLogin_Click(object sender, EventArgs e)
        {
            // Retrieve form input values
            string usernameInput = usernameTB.Value;
            string passwordInput = passwordTB.Value;

            // Check if either input is empty or null
            if (string.IsNullOrWhiteSpace(usernameInput) || string.IsNullOrWhiteSpace(passwordInput))
            {
                ShowError("Username and password are required.");
                return;
            }

            // Connect to SQL Server database
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();
                    // Query to retrieve user ID, password, and role
                    string query = "SELECT UserID, PasswordHash, UserRole FROM Users WHERE (Username = @username OR Email = @username)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", usernameInput);
                        // Execute the query and read the results
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if the user exists in the database
                            if (reader.HasRows)
                            {
                                reader.Read();
                                int userId = (int)reader["UserID"];
                                string storedHash = reader["PasswordHash"].ToString();
                                string role = reader["UserRole"].ToString();

                                // Verify the password
                                if (VerifyPassword(passwordInput, storedHash))
                                {
                                    // Successful login, set session variable and redirect
                                    Session["UserId"] = userId;
                                    Session["UserRole"] = role;
                                    // Redirect to the home page
                                    Response.Redirect("Default.aspx");
                                }
                                else
                                {
                                    ShowError("Incorrect password.");
                                }
                            }
                            else
                            {
                                ShowError("Invalid username.");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    ShowError($"Database connection error: {ex.Message}"); 
                }
                finally
                {
                    connection.Close(); 
                }
            }
        }
    }
}
