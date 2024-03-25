<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyWorkPage.aspx.cs" Inherits="CldvPoePartOne.MyWorkPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Crafts</title>
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
    </style>
</head>
<body>

    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand" href="#">KhumaloCraft</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="default.aspx">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="About.aspx">About Us</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Contact.aspx">Contact Us</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <!------------------------------------Container------------------------------------------------->
    <div class="container" style="background-color:azure">
        <h1 class="mt-5 mb-4">Explore Our Craftwork</h1>

          <!---------------------------------Brief description------------------------------>

  <p class="card-text">KhumaloCraft Emporium is committed to displaying handcrafted products from craftsmen throughout the world. We offer an environment for artists to showcase their distinctive works, connecting them to a global audience of sophisticated clients.
      KhumaloCraft's purpose is to showcase craftsmanship, support craftsmen, and provide clients with unique, high-quality handmade goods.
      Through this platform, we hope to promote cultural variety, conserve traditional crafts, and economically empower craftspeople.</p>

        <div class="row">
            <div class="col-md-4">
                <div class="craft-item">
                    <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSKy8x-SWDwlGUimXN7O8KivnagN_mztU3ZbA&usqp=CAU" alt="Craft 1" class="img-fluid">
                    <h2>My personas</h2>
                    <p>this is how we wake up every diffent day.</p>
                    <p>Author: Zacarias Antero</p>
                    <p>Price: R299.99</p>
                    <button type="button" class="btn btn-primary">Buy</button>
                </div>
            </div>
            <div class="col-md-4">
                <div class="craft-item">
                    <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHGa1-ecOXXvJauYya458R-KLJoP4fVnTENA&usqp=CAU" alt="Craft 2" class="img-fluid">
                    <h2>Elephent pot</h2>
                    <p>Very good for decoration but not to be used to cook elephants please.</p>
                    <p>Author: James Swaxi</p>
                    <p>Price: R450.50</p>
                    <button type="button" class="btn btn-primary">Buy</button>
                </div>
            </div>
            <div class="col-md-4">
                <div class="craft-item">
                    <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSfMFUBxUVkKtm2EaN_J7Omz5Y6Y3yH44_-0-EKwe-LSOTYlB-tGnx58ub5Ysug5anWslI&usqp=CAU" alt="The thinker" class="img-fluid">
                    <h2>The thinkers </h2>
                    <p>Students thinking about their life.</p>
                    <p>Author: Zacarias Antero</p>
                    <p>Price: R299.25</p>
                     <button type="button" class="btn btn-primary">Buy</button>
                </div>
            </div>
           
             <div class="col-md-4">
     <div class="craft-item">
         <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSvnR5a-UbT-Q1VLaXpIpw1S_H8IU9_sWmO9A&usqp=CAU" alt="bidwork" class="img-fluid">
         <h2>bidwork bracellets </h2>
         <p>Good gift for valentines day.</p>
         <p>Author: Khumalo Zimbungu</p>
         <p>Price: R199.25</p>
          <button type="button" class="btn btn-primary">Buy</button>
        </div>
    </div>
    <div class="col-md-4">
     <div class="craft-item">
         <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRlpDYwN32L3IV3kWVfI2Vcs9iAjlNrvzlPMQ&usqp=CAU" alt="Dress" class="img-fluid">
         <h2>Bidwork  </h2>
         <p>Stylus bidwork dress.</p>
         <p>Author: James Khumalo </p>
         <p>Price: R699.25</p>
          <button type="button" class="btn btn-primary">Buy</button>
      </div>
    </div>
    <div class="col-md-4">
     <div class="craft-item">
         <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQkZFvyb26VYxuNmhf3njkbKCkrS6FKA4M2zw&usqp=CAU" alt="The mighty African army" class="img-fluid">
         <h2>The mighty African men </h2>
         <p>African men ready to protect the land.</p>
           <p>Author: James Khumalo </p>
         <p>Price: 999.99</p>
          <button type="button" class="btn btn-primary">Buy</button>
     </div>
    </div>
     </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
