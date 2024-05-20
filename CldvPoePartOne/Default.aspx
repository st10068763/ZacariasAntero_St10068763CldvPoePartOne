<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CldvPoePartOne._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>    
    <!-- Custom CSS -->
    <link rel="stylesheet" href="~/Scripts/MyStyleSheet.css"/>

    <div class="container" style="background-color: #343a40; color: #f5f5f5; padding: 20px; border-radius: 10px; box-shadow: 0px 10px 15px rgba(0, 0, 0, 0.3);">
        <div class="row">
            <div class="col-md-12">
                <div class="jumbotron text-center" style="background-color: #333; color: #f5f5f5;">
                    <h1 class="display-4">Welcome to KhumaloCraft Emporium</h1>
                    <p class="lead">Discover Great Handcrafted Treasures from Artisans Around the World!</p>
                    <hr class="my-4" style="border-color: #555;"> <!-- Darker divider -->
                    <p class="lead">
                        <a class="btn btn-info btn-lg" href="http://www.asp.net" role="button">Learn more</a> <!-- Lighter blue for buttons -->
                    </p>
                </div>
            </div>
        </div>

        <!------------------------------------Explore craft---------------------------------->
        <div class="row">
            <div class="col-md-4">
                <div class="card mb-4 shadow-sm" style="background-color: #2c2c2c; color: #f5f5f5;">
                    <div class="card-body">
                        <h2 class="card-title">Explore Craftwork</h2>
                        <p class="card-text">Browse our collection of handcrafted products from artisans worldwide.</p>
                        <a href="MyWorkPage.aspx" class="btn btn-info">View Craftwork</a>
                    </div>
                </div>
            </div>

            <!------------------------------------About Us---------------------------------->
            <div class="col-md-4">
                <div class="card mb-4 shadow-sm" style="background-color: #2c2c2c; color: #f5f5f5;">
                    <div class="card-body">
                        <h2 class="card-title">About us</h2>
                        <p class="card-text">Learn more about KhumaloCraft Emporium and our mission.</p>
                        <a href="About.aspx" class="btn btn-info">About us</a>
                    </div>
                </div>
            </div>

            <!------------------------------------Contact us---------------------------------->
            <div class="col-md-4">
                <div class="card mb-4 shadow-sm" style="background-color: #2c2c2c; color: #f5f5f5;">
                    <div class="card-body">
                        <h2 class="card-title">Contact us</h2>
                        <p class="card-text">Have questions or feedback? Reach out to us at any time!</p>
                        <a href="Contact.aspx" class="btn btn-info">Contact us</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</asp:Content>
