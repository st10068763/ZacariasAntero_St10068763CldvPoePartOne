using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CldvPoePartOne
{
    public partial class SigninPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Signin_Click(object sender, EventArgs e)
        {
            string nameInput = name.Value;
            string emailInput = email.Value;
            string passwordInput = password.Value;
            string roleInput = role.Value;

            if (string.IsNullOrWhiteSpace(nameInput) ||
                string.IsNullOrWhiteSpace(emailInput) ||
                string.IsNullOrWhiteSpace(passwordInput) ||
                string.IsNullOrWhiteSpace(roleInput))
            {
                ShowError("All fields are required.");
                return;
            }

            string connectionString = "@Data Source=sqlserverkhumaloscrafs.database.windows.net;Initial Catalog=khumaloCraftsDB;Persist Security Info=True;User ID=st10068763Zacarias;Password=***********;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if email already exists
                    string checkQuery = "SELECT email FROM Users WHERE email = @Email";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Email", emailInput);

                        SqlDataReader reader = checkCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            ShowError("Email already in use.");
                            return;
                        }
                    }

                    // If email is unique, insert new user into the database
                    string passwordHash = HashPassword(passwordInput); // Implement a secure password hashing function
                    string insertQuery = "INSERT INTO Users (name, email, password_hash, role) VALUES (@Name, @Email, @PasswordHash, @role)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@User_Name", nameInput);
                        insertCommand.Parameters.AddWithValue("@Email", emailInput);
                        insertCommand.Parameters.AddWithValue("@Password_hash", passwordHash);
                        insertCommand.Parameters.AddWithValue("@role", roleInput);

                        insertCommand.ExecuteNonQuery(); // Execute the query
                    }

                    // Successful sign-in, redirect to login or dashboard
                    Response.Redirect("LoginPage.aspx");
                }
                catch (SqlException ex)
                {
                    ShowError($"Database connection error: {ex.Message}");
                }
            }
        }

        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

        private string HashPassword(string password)
        {
            // Implement a secure password hashing function
            // For example, use bcrypt, PBKDF2, or similar
            // This is a simple example and not secure in production
            return password; // Just an example
        }
    }
}
