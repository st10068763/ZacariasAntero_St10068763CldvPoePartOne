<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="CldvPoePartOne.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - KhumaloCraft</title>
    <style>
        /* Same styling as previous example */
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 100%;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #eaeaea;
        }

        .login-box {
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 10px 15px rgba(0, 0, 0, 0.1);
            text-align: center;
            width: 300px;
        }

        .input-field {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .login-button {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .login-button:hover {
            background-color: #45a049;
        }

        .error-message {
            color: red;
            font-weight: bold;
            margin-top: 10px;
        }
        .auto-style1 {
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 10px 15px rgba(0, 0, 0, 0.1);
            text-align: center;
            width: 344px;
            height: 343px;
        }
    </style>

    <!-- Include client-side validation script -->
    <script>
        function validateForm() {
            const usernameField = document.getElementById("username");
            const passwordField = document.getElementById("password");
            const errorMessage = document.getElementById("error-message");

            if (usernameField.value.trim() === "" || passwordField.value.trim() === "") {
                errorMessage.innerText = "Username and password are required.";
                return false; // Prevent form submission
            }

            errorMessage.innerText = ""; // Clear any previous errors
            return true; // Allow form submission
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateForm()">
        <div class="container">
            <div class="auto-style1">
                <h2>Login</h2>
                <input type="text" runat="server" id="username" class="input-field" placeholder="Username or Email" />
                <input type="password" runat="server" id="password" class="input-field" placeholder="Password" />
                <button type="submit" class="login-button">Login</button>
                <div id="error-message" class="error-message">
                </div>
                <div class="signin-link">
                    Don't have an account? <a href="SigninPage.aspx">Sign In</a>
                </div>
            </div>

        </div>

    </form>
</body>
</html>
