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
            string connectionString = "Your_SQL_Server_Connection_String";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT user_id, password_hash FROM Users WHERE (name = @username OR email = @username)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", usernameInput);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read(); // Read the first row
                            string storedHash = reader["password_hash"].ToString(); // Get the stored password hash

                            // Validate password with the stored hash
                            bool isPasswordCorrect = VerifyPassword(passwordInput, storedHash);
                            if (isPasswordCorrect)
                            {
                                // Successful login, set session variable and redirect
                                Session["UserId"] = reader["user_id"];
                                Response.Redirect("Dashboard.aspx");
                            }
                            else
                            {
                                ShowError("Invalid username or password."); // Display error message
                            }
                        }
                        else
                        {
                            ShowError("Invalid username or password."); // Display error message
                        }
                    }
                }
                catch (SqlException ex)
                {
                    ShowError($"Database connection error: {ex.Message}"); // Handle SQL exceptions
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
