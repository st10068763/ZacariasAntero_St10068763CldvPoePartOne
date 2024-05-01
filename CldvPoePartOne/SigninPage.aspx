<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SigninPage.aspx.cs" Inherits="CldvPoePartOne.SigninPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In - KhumaloCraft</title>
    <style>
        /* Basic styling for the sign-in page */
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

        .signin-box {
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 10px 15px rgba(0, 0, 0, 0.1);
            text-align: center;
            width: 350px;
        }

        .signin-box h2 {
            margin-bottom: 20px;
        }

        .input-field {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .signin-button {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .signin-button:hover {
            background-color: #45a049;
        }

        .role-select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .error-message {
            color: red;
            font-weight: bold;
            margin-top: 10px;
        }
    </style>

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
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateForm()">
        <div class="container">
            <div class="signin-box">
                <h2>Sign In</h2>
                <!-- Form fields -->
                <input type="text" runat="server" id="name" class="input-field" placeholder="Name" />
                <input type="email" runat="server" id="email" class="input-field" placeholder="Email" />
                <input type="password" runat="server" id="password" class="input-field" placeholder="Password" />
                <!-- Role selection -->
                <select id="role" runat="server" class="role-select">
                    <option value="">Select Role</option>
                    <option value="buyer">Buyer</option>
                    <option value="seller">Seller</option>
                </select>
                <!-- Submit button -->
                <button type="submit" class="signin-button">Sign In</button>
                <div id="error-message" class="error-message"></div>
            </div>
        </div>
    </form>
</body>
</html>
