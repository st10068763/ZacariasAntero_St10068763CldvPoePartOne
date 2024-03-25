<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CldvPoePartOne._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="jumbotron text-center">
                    <h1 class="display-4">Welcome to KhumaloCraft Emporium</h1>
                    <p class="lead">Discover Handcrafted Treasures from Artisans Around the World</p>
                    <hr class="my-4">
                    <p class="lead">
                        <a class="btn btn-primary btn-lg" href="http://www.asp.net" role="button">Learn more</a>
                    </p>
                </div>
            </div>
        </div>
        <!------------------------------------Explore craft---------------------------------->
        <div class="row">
            <div class="col-md-4">
                <div class="card mb-4 shadow">
                    <div class="card-body">
                        <h2 class="card-title">Explore Craftwork</h2>
                        <p class="card-text">Browse our collection of handcrafted products from artisans worldwide.</p>
                        <a href="MyWorkPage.aspx" class="btn btn-primary">View Craftwork</a>
                    </div>
                </div>
            </div>
            <!------------------------------------About Us---------------------------------->
            <div class="col-md-4">
                <div class="card mb-4 shadow">
                    <div class="card-body">
                        <h2 class="card-title">About Us</h2>
                        <p class="card-text">Learn more about KhumaloCraft Emporium and our mission.</p>
                        <a href="About.aspx" class="btn btn-primary">About Us</a>
                    </div>
                </div>
            </div>
            <!------------------------------------Contact us---------------------------------->
            <div class="col-md-4">
                <div class="card mb-4 shadow">
                    <div class="card-body">
                        <h2 class="card-title">Contact Us</h2>
                        <p class="card-text">Have questions or feedback? Reach out to us!</p>
                        <a href="Contact.aspx" class="btn btn-primary">Contact Us</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
