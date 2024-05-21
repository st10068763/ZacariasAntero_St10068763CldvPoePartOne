<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SigninPage.aspx.cs" Inherits="CldvPoePartOne.SigninPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In - KhumaloCraft</title>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>    
    <!-- Custom CSS -->
    <link rel="stylesheet" href="~/Scripts/MyStyleSheet.css"/>

    <style>
        body {
             font-family: 'Arial', sans-serif;
             background-color: #f8f9fa;
             padding-top: 20px;
        }
        .container {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .signin-box {           
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2); /* Modern shadow for visibility */
            text-align: center;
            width: 100%;
            max-width: 450px;
        }
        .signin-box h2 {
            margin-bottom: 20px;
            font-size: 24px;
            font-weight: bold;
            color: white;
           
            padding: 10px;
            border-radius: 10px;
            display: inline-block;
        }
        .input-field {
            margin-bottom: 20px;
            border-radius: 10px;
            padding: 15px;
            font-size: 16px;
            background-color: rgba(255, 255, 255, 0.8); /* Semi-transparent input background */
        }
        .signin-button {
            background-color: #007bff;
            color: white;
            padding: 15px 20px;
            border: none;
            border-radius: 10px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            transition: background-color 0.3s ease;
        }
        .signin-button:hover {
            background-color: #0056b3;
        }
        .role-select {
            height: 45px;
            border-radius: 10px;
            padding: 10px;
            font-size: 16px;
            background-color:black); /* Semi-transparent select background */
        }
        .error-message {
            color: red;
            font-weight: bold;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateForm()">
        <div class="container">
            <div class="signin-box">
                <h2>Sign In</h2>
                <!-- Form fields -->
                <input type="text" runat="server" id="name" class="input-field form-control" placeholder="Enter user name" />
                <input type="email" runat="server" id="email" class="input-field form-control" placeholder="Enter user email" />
                <input type="password" runat="server" id="password" class="input-field form-control" placeholder="Enter user password" />
                <!-- Role selection -->
                <label for="role" class="sr-only">Select role</label>
                <select id="role" runat="server" class="role-select form-control">
                    <option value="">Select Role</option>
                    <option value="buyer">Buyer</option>
                    <option value="seller">Seller</option>
                </select>
                <!-- Sign in button -->
                <button type="submit" runat="server" onserverclick="Signin_Click" class="signin-button btn btn-primary btn-block mt-4">Create Account</button>

                <!-- link to the login page -->
                <a href="LoginPage.aspx" class="btn btn-link mt-3">Already have an account? Login</a>
                
                <div id="error-message" class="error-message mt-3"></div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>

    <script>
        function validateForm() {
            const nameField = document.getElementById("name");
            const emailField = document.getElementById("email");
            const passwordField = document.getElementById("password");
            const roleField = document.getElementById("role");
            const errorMessage = document.getElementById("error-message");

            if (nameField.value.trim() === "" ||
                emailField.value.trim() === "" ||
                passwordField.value.trim() === "" ||
                roleField.value === "") {
                errorMessage.innerText = "All fields are required.";
                return false;
            }

            errorMessage.innerText = ""; // Clear previous errors
            return true; // Allow form submission
        }
    </script>
</body>
</html>
