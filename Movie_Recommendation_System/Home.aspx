 <%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Movie_Recommendation_System.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Movie Recommendation System</title>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
/*        body{
            background-color:black;
            color:white;
        }*/
        .movie-card img {
/*            background-color:black;
            color:white;*/
            max-height: 200px;
            object-fit: cover;
        }

        .jumbotron {
/*            background-color: black;*/
            background-color: #f8f9fa;
            padding: 2rem;
            border-radius: .3rem;
        }

        .card {
/*            background-color:black;
            color:white;*/
            border-radius: 15px;
            display: flex;
            flex-direction: column;
            overflow: hidden;
            width: 100%; /* Make sure the card itself stretches across the container */
            height: 100%;
        }

        .card-body {
            flex: 1; /* Make the card body take up remaining space */
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .card-title {
            font-weight: bold;
        }

        .profile-icon {
            font-size: 2rem;
            cursor: pointer;
            color: #000;
        }

        .profile-icon:hover {
            color: #007bff;
        }

        .navbar {
            margin-bottom: 20px;
        }

        .btn-logout {
            font-size: 1rem;
            color: #dc3545;
            border: 1px solid #dc3545;
            border-radius: .25rem;
            background: white;
            margin-right: 20px;
        }

        .btn-logout:hover {
                color: #c82333;
                background-color: #f8d7da;
                border-color: #c82333;
            }

        .nav-icon-button {
            display: flex;
            align-items: center;
        }

        .admin-link {
            font-size: 1.2rem;
            display: flex;
            justify-content: center;
            align-items: center;
            color: blue;
            margin-right: 20px;
            text-decoration: none;
        }

         .admin-link:hover {
                text-decoration: underline; /* Optional: Add underline on hover */
            }

        .movie-card img {
/*            background-color:black;
            color:white;*/
            width: 100%; /* Stretch the image to fill the width of the card */
            height: 100%; /* Set a fixed height for the image */
            object-fit: cover; /* Ensures the image covers the area without stretching */
        }

        .btn-primary {
            background-color: #007bff;
            border: none;
            color: white;
            padding: 10px 15px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            border-radius: 4px;
            transition: background-color 0.3s ease;
        }

        .btn-primary:hover {
            background-color: #0056b3;
            text-decoration: none;
        }

        .wishlist-icon {
            font-size: 24px;
            color: #28a745;
            cursor: pointer;
        }
        .wishlist-icon:hover {
            color: #218838;
        }
        .btn-wishlist{
            margin-right:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid p-5">

            <!------------------------------------------- Navbar with Profile Icon ------------------------------------------>

            <nav class="navbar navbar-light bg-light">
                <a class="navbar-brand" href="#">MovieRecs</a>
                <div class="ml-auto d-flex align-items-center">
                    <asp:Panel ID="adminPanel" runat="server">
                        <asp:HyperLink ID="adminHyperLink" runat="server" NavigateUrl="AdminPanel.aspx" Text="Admin Panel" CssClass="admin-link"></asp:HyperLink>
                    </asp:Panel>
                    <asp:Button ID="btnWishlist" runat="server" CssClass="btn btn-wishlist btn-success" Text="My Wishlist" OnClick="btnWishlist_Click" />
                    <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-logout" Text="Logout" OnClick="btnLogout_Click" />
                    <span class="profile-icon">
                        <i class="fa fa-user-circle" aria-hidden="true" title="Profile"></i>
                    </span>
                </div>
            </nav>


            <div class="jumbotron mt-4">
                <h1 class="display-4">Welcome to MovieRecs!</h1>
                <p class="lead">Get personalized movie recommendations based on your taste.</p>
            </div>
            <hr class="my-4" />

            <!------------------------------------------------search-baar------------------------------------------------------>

            <div class="mb-1">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control" Placeholder="Search by movie name" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlSearchGenre" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Genre" Value="-1" />
                                <asp:ListItem Text="Action" Value="Action" />
                                <asp:ListItem Text="Comedy" Value="Comedy" />
                                <asp:ListItem Text="Drama" Value="Drama" />
                                <asp:ListItem Text="Horror" Value="Horror" />
                                <asp:ListItem Text="Sci-Fi" Value="Sci-Fi" />
                                <asp:ListItem Text="Thriller" Value="Thriller" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlSearchLanguage" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Language" Value="-1" />
                                <asp:ListItem Text="English" Value="English" />
                                <asp:ListItem Text="Hindi" Value="Hindi" />
                                <asp:ListItem Text="Gujarati" Value="Gujarati" />
                                <asp:ListItem Text="Tamil" Value="Tamil" />
                                <asp:ListItem Text="Kannada" Value="Kannada" />       
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" Text="Search" AutoPostBack="true" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>

            <!-----------------------------------Searched Movies------------------------------------------------------------>

            <asp:Label ID="searcherror" ForeColor="Red" runat="server"></asp:Label>
            <div class="row mb-1">
                <asp:Repeater ID="searchRepeater" runat="server" OnItemDataBound="MoviesRepeater_SetAttribute">
                    <ItemTemplate>
                        <div class="col-md-3 mb-4">
                            <div class="card">
                                <asp:Image ID="imgPoster" runat="server" CssClass="card-img-top movie-card-img" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Poster")) %>'/> 
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Name") %></h5>
                                    <p class="card-text">
                                        IMDb: <%# Eval("IMDb") %><br />
                                        Genre: <%# Eval("Genre") %><br />
                                    </p>
                                    <div>
                                    <asp:Button ID="btnDetails" runat="server" Text="Details"
                                        OnClick="btnDetails_Click" 
                                        CommandArgument='<%# Eval("Id") + "|" + Eval("Genre") %>'
                                        CssClass="btn btn-primary" />
                                        <!-- Add to Wishlist Button -->
                                        <asp:Button ID="btnAddToWishlist" runat="server" Text="Add to Wishlist" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary btn-sm" OnClick="lnkWishlist_Click" />
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <br />

            <!--------------------------------------Recommended Movies------------------------------------------------------>
            <h2 class="mb-4">Recommended Movies</h2>
            <asp:Label ID="recoerrormsg" ForeColor="Red" runat="server"></asp:Label>
            <div class="row">
                <asp:Repeater ID="RecoMoviesRepeater" runat="server" OnItemDataBound="MoviesRepeater_SetAttribute">
                    <ItemTemplate>
                        <div class="col-md-3 mb-4">
                            <div class="card">
                                <asp:Image ID="imgPoster" runat="server" CssClass="card-img-top movie-card-img" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Poster")) %>' /> 
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Name") %></h5>
                                    <p class="card-text">
                                        IMDb: <%# Eval("IMDb") %><br />
                                        Genre: <%# Eval("Genre") %><br />
                                    </p>
                                    <div>
                                    <asp:Button ID="btnDetails" runat="server" Text="Details"
                                        OnClick="btnDetails_Click" 
                                        CommandArgument='<%# Eval("Id") + "|" + Eval("Genre") %>'
                                        CssClass="btn btn-primary" />
                                        <!-- Add to Wishlist Button -->
                                        <asp:Button ID="btnAddToWishlist" runat="server" Text="Add to Wishlist" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary btn-sm" OnClick="lnkWishlist_Click" />
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <br /><br />
            <h2 class="mb-4">OtherMovies</h2>
            <div class="row">
                <asp:Repeater ID="MoviesRepeater" runat="server" OnItemDataBound="MoviesRepeater_SetAttribute">
                    <ItemTemplate>
                        <div class="col-md-3 mb-4">
                            <div class="card">
                               <asp:Image ID="imgPoster" runat="server" CssClass="card-img-top movie-card-img" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Poster")) %>' /> 
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Name") %></h5>
                                    <p class="card-text">
                                        IMDb: <%# Eval("IMDb") %><br />
                                        Genre: <%# Eval("Genre") %><br />
                                    </p>
                                    <div>
                                    <asp:Button ID="btnDetails" runat="server" Text="Details"
                                        OnClick="btnDetails_Click" 
                                        CommandArgument='<%# Eval("Id") + "|" + Eval("Genre") %>'
                                        CssClass="btn btn-primary" />
                                        <!-- Add to Wishlist Button -->
                                        <asp:Button ID="btnAddToWishlist" runat="server" Text="Add to Wishlist" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary btn-sm" OnClick="lnkWishlist_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>
    </form>


    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/js/all.min.js"></script>
</body>
</html>
