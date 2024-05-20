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


        /// <summary>
        /// Sign in button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Signin_Click(object sender, EventArgs e)
        {
            // Get user input from the form
            string nameInput = name.Value;
            string emailInput = email.Value;
            string passwordInput = password.Value;
            string roleInput = role.Value;

            // Validate input to ensure no empty fields
            if (string.IsNullOrWhiteSpace(nameInput) ||
                string.IsNullOrWhiteSpace(emailInput) ||
                string.IsNullOrWhiteSpace(passwordInput) ||
                string.IsNullOrWhiteSpace(roleInput))
            {
                // Show an error message if any field is empty as they are all required
                ShowError("All fields are required.");
                return;
            }

            // this method will insert the new user into the database
            InsertUser(nameInput, emailInput, passwordInput, roleInput);

        }

        // Method to insert new user into the database
        private void InsertUser(string name, string email, string password, string role)
        {
            // connection string 
            string connectionString = "Data Source=sqldatabasekhumalo.database.windows.net;Initial Catalog=khumaloDatabase;Persist Security Info=True;User ID=st10068763;Password=MyName007";
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Check if email already exists
                    string checkQuery = "SELECT User_Name, Email FROM Users WHERE User_Name = @Name OR Email =@Email";

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        checkCommand.Parameters.AddWithValue("@Email", email);

                            using (SqlDataReader reader = checkCommand.ExecuteReader())

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string storedName = reader["User_Name"].ToString();
                                    string storedEmail = reader["Email"].ToString();

                                    if (email.Equals(storedEmail,StringComparison.OrdinalIgnoreCase))
                                    {
                                        ShowError("email already exist, choose another.");
                                        return;
                                    }
                                    if (storedEmail == email)
                                    {
                                        ShowError("Email already exist, choose another.");
                                        return;
                                    }
                                }
                               
                            }
                    }


                    // Implement a secure password hashing function
                    string passwordHash = HashPassword(password);
                    // Insert the new user into the database
                    string insertQuery = "INSERT INTO Users (User_Name, Email, Password_hash, role) VALUES (@Name, @Email, @PasswordHash, @Role)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@Email", email);
                        insertCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        insertCommand.Parameters.AddWithValue("@Role", role);
                        // Execute the command to insert the new user in the database
                        insertCommand.ExecuteNonQuery();
                    }

                    // Successful sign-in, redirect to login or dashboard
                    Response.Redirect("LoginPage.aspx");
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

        // method to create a pop up alert
        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }
        // method to hash the password
        private string HashPassword(string password)
        {
            return password; 
        }
    }
}
