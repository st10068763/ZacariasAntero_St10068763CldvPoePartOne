using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace CldvPoePartOne
{
    public partial class SignupPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Sign up button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignup_click(object sender, EventArgs e)
        {
            // Get user input from the form
            string usernameInput = usernameTB.Value;
            string emailInput = email.Value;
            string passwordInput = password.Value;
            string firstnameInput = firstname.Value;
            string lastnameInput = lastname.Value;
            string phonenumberInput = phonenumber.Value;
            string locationInput = location.Value;
            string roleInput = role.Value;

            // Validate input to ensure no empty fields
            if (string.IsNullOrWhiteSpace(usernameInput) ||
                string.IsNullOrWhiteSpace(emailInput) ||
                string.IsNullOrWhiteSpace(passwordInput) ||
                string.IsNullOrWhiteSpace(firstnameInput) ||
                string.IsNullOrWhiteSpace(lastnameInput) ||
                string.IsNullOrWhiteSpace(phonenumberInput) ||
                string.IsNullOrWhiteSpace(locationInput) ||
                string.IsNullOrWhiteSpace(roleInput))
            {
                // Show an error message if any field is empty as they are all required
                ShowError("All fields are required.");
                return;
            }

            // Insert the new user into the database
            InsertUser(usernameInput, emailInput, passwordInput, firstnameInput, lastnameInput, phonenumberInput, locationInput, roleInput);
        }

        // Method to insert new user into the database
        private void InsertUser(string username, string email, string password, string firstname, string lastname, string phonenumber, string location, string role)
        {
            // Connection string
            string connectionString = "Data Source=newkhumaloserver.database.windows.net;Initial Catalog=newkhumaloDb;User ID=st10068763;Password=MyName007";

            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Check if email already exists
                    string checkQuery = "SELECT Username, Email FROM Users WHERE Username = @Username OR Email = @Email";

                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);
                        checkCommand.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = checkCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string storedUsername = reader["Username"].ToString();
                                    string storedEmail = reader["Email"].ToString();

                                    if (email.Equals(storedEmail, StringComparison.OrdinalIgnoreCase))
                                    {
                                        ShowError("Email already exists, choose another.");
                                        return;
                                    }

                                    if (storedUsername.Equals(username, StringComparison.OrdinalIgnoreCase))
                                    {
                                        ShowError("Username already exists, choose another.");
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    // Implement a secure password hashing function
                    string passwordHash = HashPassword(password);

                    // Insert the new user into the database
                    string insertQuery = "INSERT INTO Users (Username, Email, FirstName, LastName, PasswordHash, UserRole, PhoneNumber, Location) VALUES (@Username, @Email, @FirstName, @LastName, @PasswordHash, @UserRole, @PhoneNumber, @Location)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@Email", email);
                        insertCommand.Parameters.AddWithValue("@FirstName", firstname);
                        insertCommand.Parameters.AddWithValue("@LastName", lastname);
                        insertCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        insertCommand.Parameters.AddWithValue("@UserRole", role);
                        insertCommand.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                        insertCommand.Parameters.AddWithValue("@Location", location);

                        // Execute the command to insert the new user in the database
                        insertCommand.ExecuteNonQuery();
                    }

                    // Successful sign-up, redirect to login or dashboard
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

        // Method to create a pop-up alert
        private void ShowError(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

        // Method to hash the password
        private string HashPassword(string password)
        {
            return password;
        }
    }
}
