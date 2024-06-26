﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyWorkPage.aspx.cs" Inherits="CldvPoePartOne.MyWorkPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Crafts - KhumaloCraft</title>
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
        }
        .craft-item {
            margin-bottom: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .craft-item img {
            max-width: 100%;
            height: auto;
        }
        .craft-item h2 {
            margin-top: 0;
            color: #333;
        }
        .craft-item p {
            color: #666;
        }
        .craft-item .buy-button {
            background-color: #28a745;
            color: white;
            padding: 10px;
            border: none;
            border-radius: 10px;
            cursor: pointer;
        }
        .craft-item .buy-button:hover {
            background-color: #218838;
        }
        .auto-style1 {
            display: block;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-clip: padding-box;
            border-radius: .25rem;
            transition: none;
            border: 1px solid #ced4da;
            background-color: #fff;
        }
    </style>
</head>
<body>
    <!-- Top Navigation bar -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand" href="#">KhumaloCraft</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item"><a class="nav-link" href="default.aspx">Home</a></li>
                    <li class="nav-item"><a class="nav-link" href="About.aspx">About Us</a></li>
                    <li class="nav-item"><a class="nav-link" href="Contact.aspx">Contact Us</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- End of Navigation bar -->

    <div class="container">
        <h2>Explore Our Craftwork</h2>

        <form id="mainForm" runat="server">
        
            <!-- Search form -->
            <div class="form-group">
                <label for="SearchTerm">Search by Product Name or Author:</label>
                <asp:TextBox ID="SearchTermTB" runat="server" CssClass="form-control" placeholder="Enter product name or author" />
            </div>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />

             <!--Product table binds the data from the database -->
             <div class="section mb-4">
                 <h2>Search Result</h2>
                 <asp:GridView ID="GridViewProducts" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                     <Columns>
                         <asp:BoundField DataField="productName" HeaderText="Product Name" />
                         <asp:BoundField DataField="productDescription" HeaderText="Description" />
                         <asp:BoundField DataField="Price" HeaderText="Price" />
                         <asp:BoundField DataField="Stock" HeaderText="Stock available" />
                         <asp:BoundField DataField="productAuthor" HeaderText="Product Author" />
                     </Columns>
                 </asp:GridView>
             </div>

            <!-- Product listing -->
            <asp:Repeater ID="ProductRepeater" runat="server">
                <ItemTemplate>
                    <div class="craft-item">
                        <img src='<%# Eval("productImage") %>' alt="Product image" class="img-fluid" />                  
                        <h2><%# Eval("productName") %></h2>
                        <p><%# Eval("productDescription") %></p>
                        <p>Author: <%# Eval("productAuthor") %></p>
                        <p>Price: R<%# Eval("Price") %></p>
                        <p>Stock: <%# Eval("Stock") %></p>                   
                        <asp:Button ID="BuyButton" runat="server" Text="Buy" CssClass="btn btn-primary" OnClick="BuyButton_Click" CommandArgument='<%# Eval("ProductID") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <h3>Add New Product</h3>

            <!-- Form to insert new products into the database -->
            <div class="form-group">
                <label for="ProductName">Product Name:</label>
                <asp:TextBox ID="ProductName" runat="server" CssClass="form-control" placeholder="Enter the product name" />
            </div>
            <div class="form-group">
                <label for="ProductAuthor">Product Author:</label>
                <asp:TextBox ID="ProductAuthorTB" runat="server" CssClass="form-control" placeholder="Enter the name of the author here" />
            </div>
            <div class="form-group">
                <label for="ProductDescription">Product Description:</label>
                <asp:TextBox ID="ProductDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Enter your product description" />
            </div>
            <div class="form-group">
                <label for="ImageURL">Product Image URL:</label>
                <asp:TextBox ID="ImageURLTB" runat="server" CssClass="form-control" placeholder="Enter the URL link of your image here" />
            </div>
            <div class="form-group">
                <label for="Price">Price:</label>
                <asp:TextBox ID="Price" runat="server" CssClass="auto-style1" Width="310px" />
            </div>
            <div class="form-group">
                <label for="Stock">Stock:</label>
                <asp:TextBox ID="Stock" runat="server" CssClass="auto-style1" Width="308px" />
            </div>
            
            <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-block" Text="Add Product" OnClick="btnAddProduct_Click" />
        </form>
    </div>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>
