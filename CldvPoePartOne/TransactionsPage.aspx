<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionsPage.aspx.cs" Inherits="CldvPoePartOne.TransactionsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transactions - KhumaloCraft</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
            padding-top: 20px;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
        }
        .transaction-item {
            margin-bottom: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .transaction-item img {
            max-width: 100%;
            height: auto;
        }
        .quantity-selector {
            display: flex;
            align-items: center;
        }
        .quantity-input {
            width: 50px;
            text-align: center;
            margin: 0 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }
        .checkout-button {
            background-color: #28a745;
            color: white;
            padding: 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .checkout-button:hover {
            background-color: #218838;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="TransactionsRepeater" runat="server" OnItemCommand="TransactionsRepeater_ItemCommand">
            <ItemTemplate>
                <div class="container">
                    <h2>Your Transactions</h2>
                    <!-- Display the selected product information -->
                    <div class="transaction-item">
                        <img src="<%# Eval("ProductImage") %>" alt="Product Image" class="img-fluid" />
                        <h2><%# Eval("ProductName") %></h2>
                        <p>Author: <%# Eval("ProductAuthor") %></p>
                       <p>Total Price: R<%# TotalPrice %></p>

                        <!-- Quantity selector -->
                        <div class="quantity-selector">
                            <label for="quantity">Quantity:</label>


                            <button type="button" class="btn btn-secondary" onclick="decreaseQuantity()">-</button>
                            <asp:TextBox ID="QuantityInput" runat="server" CssClass="quantity-input" Text="1" />
                            <button type="button" class="btn btn-secondary" onclick="increaseQuantity()">+</button>
                        </div>
                        
                        <!-- Total price -->
                        
                        
                        <!-- Checkout button -->
                        <button type="button" runat="server" onserverclick="CheckoutButton_Click" class="checkout-button">Checkout</button>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    <div class="container">
        <h2>Your Transactions</h2>
        <!-- Display the selected product information -->
        <div class="transaction-item"> 
            <!-- display selected product information -->
            
            <img src="<%# ProductImage %>" alt="Product Image" class="img-fluid" />
            <h2><%# ProductName %></h2>
            <p>Author: <%# ProductAuthor %></p>
            <p>Price: R<%# ProductPrice %></p>

            <!-- Quantity selector -->
            <div class="quantity-selector">
                <label for="quantity">Quantity:</label>
                <button type="button" class="btn btn-secondary" onclick="decreaseQuantity()">-</button>
                <asp:TextBox ID="QuantityInput" runat="server" CssClass="quantity-input" Text="1" />
                <button type="button" class="btn btn-secondary" onclick="increaseQuantity()">+</button>
            </div>
            
            <!-- Total price -->
            <p>Total Price: R<%# ProductPrice * int.Parse(QuantityInput.Text) %></p>
            
            <!-- Checkout button -->
            <button type="button" runat="server" onserverclick="CheckoutButton_Click" class="checkout-button">Checkout</button>
        </div>
    </div>
       

    <script>
        function decreaseQuantity() {
            var quantityInput = document.getElementById('<%# QuantityInput.ClientID %>');  // ClientID required
            var currentValue = parseInt(quantityInput.value, 10);

            if (currentValue > 1) {
                currentValue--;
                quantityInput.value = currentValue;
            }
        }

        function increaseQuantity() {
            var quantityInput = document.getElementById('<%# QuantityInput.ClientID %>');
            var currentValue = parseInt(quantityInput.value, 10);

            currentValue++;
            quantityInput.value = currentValue;
        }
    </script>
         </form>

    <!-- Scripts -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
