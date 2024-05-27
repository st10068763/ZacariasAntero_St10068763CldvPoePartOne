<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionsPage.aspx.cs" Inherits="CldvPoePartOne.TransactionsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transactions - KhumaloCraft</title>
   
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
            color: #333; 
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
        <asp:Repeater ID="ProductRepeater" runat="server" OnItemCommand="ProductRepeater_ItemCommand">
            <ItemTemplate>
                <div class="container">
                    <h2>Your Transactions</h2>
                    <div class="transaction-item">
                        <img src='<%# Eval("Product_Image") %>' alt="Product Image" class="img-fluid" />
                        <h2><%# Eval("Product_Name") %></h2>
                        <p>Author: <%# Eval("Author") %></p>
                        <p>Price: R<%# Eval("Price") %></p>
                        <div class="quantity-selector">
                            <label for="quantity">Quantity:</label>
                            <button type="button" class="btn btn-secondary" onclick="decreaseQuantity(this)">-</button>
                            <asp:TextBox ID="QuantityInput" runat="server" CssClass="quantity-input" Text="1" />
                            <button type="button" class="btn btn-secondary" onclick="increaseQuantity(this)">+</button>
                        </div>
                        <p>Total Price: R<asp:Label ID="TotalPriceLabel" runat="server" Text='<%# Eval("Price") %>' /></p>
                        <asp:Button ID="CheckoutButton" runat="server" CommandName="Checkout" CommandArgument='<%# Eval("Product_ID") %>' Text="Checkout" CssClass="checkout-button" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>

    <script>
        function decreaseQuantity(button) {
            var container = button.closest('.transaction-item');
            var quantityInput = container.querySelector('.quantity-input');
            var currentValue = parseInt(quantityInput.value, 10);

            if (currentValue > 1) {
                currentValue--;
                quantityInput.value = currentValue;
            }
            updateTotalPrice(container);
        }

        function increaseQuantity(button) {
            var container = button.closest('.transaction-item');
            var quantityInput = container.querySelector('.quantity-input');
            var currentValue = parseInt(quantityInput.value, 10);

            currentValue++;
            quantityInput.value = currentValue;
            updateTotalPrice(container);
        }

        function updateTotalPrice(container) {
            var quantityInput = container.querySelector('.quantity-input');
            var totalPriceLabel = container.querySelector('.total-price-label');
            var pricePerUnit = parseFloat(container.querySelector('.price-label').textContent.replace('R', ''));
            var totalPrice = pricePerUnit * parseInt(quantityInput.value, 10);

            totalPriceLabel.innerText = 'R' + totalPrice.toFixed(2);
        }
    </script>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>
