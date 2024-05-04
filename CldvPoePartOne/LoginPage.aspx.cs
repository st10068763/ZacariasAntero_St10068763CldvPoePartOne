using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace CldvPoePartOne
{
    public partial class LoginPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialization code, if needed
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            // Retrieve form input values
            string usernameInput = username.Value;
            string passwordInput = password.Value;

            // Check if either input is empty or null
            if (string.IsNullOrWhiteSpace(usernameInput) || string.IsNullOrWhiteSpace(passwordInput))
            {
                ShowError("Username and password are required.");
                return; // Exit early
            }

            // Connect to SQL Server database
            string connectionString =  "Data Source=sqlserverkhumaloscrafs.database.windows.net;Initial Catalog=khumaloCraftsDB;Persist Security Info=True;User ID=st10068763Zacarias;Password=MyVC@007;Encrypt=True;TrustServerCertificate=True";
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();
                    // Query to retrieve user ID and password 
                    string query = "SELECT User_ID, Password_hash FROM Users WHERE (User_Name = @username OR Email = @username)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", usernameInput);
                       // Execute the query and read the results
                       using (SqlDataReader reader = command.ExecuteReader())
                        // check is the user exists in the database
                        if (reader.HasRows)
                        {
                            reader.Read(); // Read the first row
                          
                            int userId = (int)reader["User_ID"]; // Get the user ID

                            string storedHash = reader["Password_hash"].ToString();

                            if(passwordInput == storedHash)
                            {
                                // Successful login, set session variable and redirect
                                Session["UserId"] = userId;
                                // Redirect to the home page
                                Response.Redirect("Default.aspx");
                            }
                            else
                            {
                                ShowError("Incorrect password."); // Display error message
                            }                           
                        }
                        // displays a message if the user does not exist
                        else
                        {
                            ShowError("Invalid username."); 
                        }                       
                    }
                }
                catch (SqlException ex)
                {
                    ShowError($"Database connection error: {ex.Message}"); // Handle SQL exceptions
                }

                finally
                {
                    connection.Close(); // Close the connection
                }
            }
        }


        // Function to show error messages in the frontend
        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

        // Function to verify password hashes (requires secure implementation)
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            // Example implementation of password verification. Adjust as needed.
            return inputPassword == storedHash; // This is just an example; use a secure method.
        }
    }
}
