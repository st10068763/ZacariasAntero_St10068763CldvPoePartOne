<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CldvPoePartOne.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <!-- Bootstrap CSS -->
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>    
     <!-- Custom CSS -->
     <link rel="stylesheet" href="~/Scripts/MyStyleSheet.css"/>

    <div class="container" style="background-color:#1c1c1c; color: #f5f5f5;"> 
        <div class="row">
            <div class="col-md-10 mx-auto">
                <div class="card shadow" style="background-color:#333; color: #f5f5f5;"> 
                    <div class="card-body">
                        <h2 class="card-title">About KhumaloCraft</h2>
                        
                        <p class="card-text">KhumaloCraft is a visionary global e-commerce platform dedicated to showcasing handcrafted products from artisans worldwide.</p>
                        <p class="card-text">Founded by James Khumalo, our mission is to connect artisans with a discerning audience who appreciate craftsmanship and unique creations.</p>
                        
                        <h3 class="card-title mt-4">Our Philosophy</h3>
                        <p class="card-text">We believe in celebrating the creativity and skill of artisans, preserving traditional crafts, and fostering sustainable practices.
                            Through our platform, artisans can reach a global audience, while customers can discover one-of-a-kind handmade treasures.</p>
                        
                        <h3 class="card-title mt-4">Our Commitment</h3>
                        <p class="card-text">KhumaloCraft Emporium is committed to displaying handcrafted products from craftsmen throughout the world.
                            We offer an environment for artists to showcase their distinctive works, connecting them to a global audience of sophisticated clients.
                            Our purpose is to showcase craftsmanship, support craftsmen, and provide clients with unique, high-quality handmade goods.</p>
                        
                        <p class="card-text">Through this platform, we hope to promote cultural variety, conserve traditional crafts, and economically empower craftspeople.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap JS and dependencies -->
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</asp:Content>
