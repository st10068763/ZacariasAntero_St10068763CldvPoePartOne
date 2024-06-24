<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="CldvPoePartOne.LoginPage"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - KhumaloCraft</title>
    
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>    
    <!-- Custom CSS -->
    <link rel="stylesheet" href="~/Scripts/MyStyleSheet.css"/>
   
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #343a40; 
        }
        .container {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .login-box {
            background-color: #ffffff; 
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 10px 15px rgba(0, 0, 0, 0.3);
            text-align: center;
            width: 100%;
            max-width: 400px;
        }
        .login-box h2 {
            color: #007bff; 
        }
        .form-control {
            border: 2px solid #007bff; 
            color: #000000;
        }
        .form-control::placeholder {
            color: #6c757d; 
        }
        .btn-success {
            background-color: #28a745;
            border-color: #28a745;
        }
        .btn-success:hover {
            background-color: #218838; 
            border-color: #1e7e34;
        }
        .error-message {
            color: red;
            font-weight: bold;
            margin-top: 10px;
        }
        .signin-link a {
            color: #007bff; 
        }
        .signin-link a:hover {
            color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateForm()">
        <div class="container">
            <div class="login-box">
                <h2>Login</h2>
                <input type="text" runat="server" id="usernameTB" class="form-control mb-3" placeholder="Username or Email" />
                <input type="password" runat="server" id="passwordTB" class="form-control mb-3" placeholder="Password" />

               <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block" Text="Login" OnClick="btnLogin_Click" />
                <div id="error-message" class="error-message"></div>

                <div class="signin-link mt-3" fontcolor="black">
                 <a href="SigninPage.aspx"> Don't have an account? Create one now.</a>
                </div>
            </div>
        </div>
    </form>
    
    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>
