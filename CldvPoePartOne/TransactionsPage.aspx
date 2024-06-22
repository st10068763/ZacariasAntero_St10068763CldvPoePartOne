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
       <asp:Repeater ID="ProductRepeater" runat="server">
            <ItemTemplate>
                <div class="container">
                    <h2>Transaction Details</h2>
                    <div class="transaction-item">
                        <img src='<%# Eval("productImage") %>' alt="Product Image" class="img-fluid" />
                        <h2><%# Eval("productName") %></h2>
                        <p>Author: <%# Eval("productAuthor") %></p>
                        <p>Price: R<%# Eval("price") %></p>
                        <p>Available Stock: <%# Eval("stock") %></p>
                        <div class="quantity-selector">
                            <asp:Label ID="QuantityLabel" runat="server" Text="Enter product quantity: " />
                            <asp:TextBox ID="QuantityTB" runat="server" placeholder="Enter the quantity" CssClass="quantity-input" Text="1" AutoPostBack="true" OnTextChanged="QuantityTB_TextChanged" />
                        </div>
                        <p>Total Price: R<asp:Label ID="TotalPriceLabel" runat="server" Text='<%# Eval("price") %>' /></p>
                    </div>
                    <!-- payment details-->
                    <div class="transaction-item">
                        <h2>Payment Details</h2>
                        <div class="form-group">
                            <label for="shippingAddressTB">Shipping address:</label>
                            <asp:TextBox ID="shippingAddressTB" runat="server" CssClass="form-control" placeholder="Enter the shipping address" />
                        </div>
                        <!-- payment methods-->
                        <div class="form-group">
                            <label for="PaymentMethodDDL">Payment Method:</label>
                            <asp:DropDownList ID="PaymentMethodDDL" runat="server" CssClass="form-control">
                                <asp:ListItem Value="CreditCard">Credit Card</asp:ListItem>
                                <asp:ListItem Value="DebitCard">Debit Card</asp:ListItem>
                                <asp:ListItem Value="EFT">EFT</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="CardNumberTB">Card Number:</label>
                            <asp:TextBox ID="CardNumberTB" runat="server" CssClass="form-control" placeholder="Enter card number" />
                        </div>
                        <div class="form-group">
                            <label for="ExpiryDateTB">Expiry Date:</label>
                            <asp:TextBox ID="ExpiryDateTB" runat="server" CssClass="form-control" placeholder="Enter card expiry date" />
                        </div>
                        <div class="form-group">
                            <label for="CVVTB">CVV:</label>
                            <asp:TextBox ID="CVVTB" runat="server" CssClass="form-control" placeholder="Enter CVV" />
                        </div>
                        <asp:Button ID="PayButton" runat="server" Text="Pay" CssClass="checkout-button" OnClick="PayButton_Click" CommandArgument='<%# Eval("ProductID") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>
