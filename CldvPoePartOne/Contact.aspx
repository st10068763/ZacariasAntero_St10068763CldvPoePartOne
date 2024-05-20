<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="CldvPoePartOne.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>    
    <!-- Custom CSS -->
    <link rel="stylesheet" href="~/Scripts/MyStyleSheet.css"/>

    <div class="container" style="background-color:#1c1c1c; color: #f5f5f5;">  <!-- Darker background with light text -->
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card shadow" style="background-color: #333; color: #f5f5f5;">  <!-- Darker card background -->
                    <div class="card-body">
                        <h2 class="card-title">Contact Us for More Information</h2>
                        <p class="card-text">Have questions or feedback? Reach out to us!</p>
                        <ul class="list-unstyled">  <!-- Changed list style for a cleaner look -->
                            <li><strong>Address:</strong> 288 Main Road, Cape Town, South Africa</li>
                            <li><strong>Phone:</strong> 0226655907</li>
                            <li><strong>Email:</strong> khumaloCraft@yahoo.com</li> 
                        </ul>
                        <h4>Follow us on Instagram: Khumalos_Craft</h4>
                        <h4>Marketing: Khumalo Craft</h4>
                        <p>Khumalos Craft</p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap JS and dependencies -->
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/js/bootstrap.bundle.min.js"></script>
</asp:Content>
