<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieDetails.aspx.cs" Inherits="Movie_Recommendation_System.MovieDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movie Details</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <style>
        /* Body Background */
        body {
            font-family: 'Poppins', sans-serif;
        }

        /* Card Container */
        .card {
            margin-top: 40px;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 10px 25px rgb(128, 128, 128);
            color: black;
            border: 1.5px double black;
        }

        /* Movie Poster Styling */
        .movie-poster {
            height: 100%;
            width: 100%;
            border-radius: 0 0 20px 20px;
            object-fit: cover;
        }

        /* Card Header */
        .card-body {
            padding: 50px 40px;
            background : rgb(238, 232, 232);
        }

        .movie-info h2 {
            font-weight: 700;
            font-size: 30px;
            margin-bottom: 20px;
            letter-spacing: 1px;
        }

        .movie-info p {
            margin-bottom: 15px;
            font-size: 16px;
            line-height: 1.8;
        }

        .movie-info strong {
            color: black;
        }

        /* Gradient Text for Title */
        .movie-title {
            background: -webkit-linear-gradient(45deg, rgb(219, 211, 211), black);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            font-size: 36px;
            font-weight: bold;
        }

        /* Responsive */
        @media (max-width: 768px) {
            .card-body {
                padding: 30px 20px;
            }

            .movie-info h2 {
                font-size: 24px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="card">
                <div class="row no-gutters">
                    <!-- Left Side: Movie Poster -->
                    <div class="col-md-4">
                        <asp:Image ID="imgPoster" runat="server" CssClass="movie-poster" />
                    </div>
                    <!-- Right Side: Movie Information -->
                    <div class="col-md-8">
                        <div class="card-body">
                            <div class="movie-info">
                                <h2 class="movie-title"><asp:Label ID="lblMovieName" runat="server" /></h2>
                                <p><strong>IMDb Rating: </strong>
                                    <span class="imdb-rating">
                                        <asp:Label ID="lblIMDb" runat="server" />
                                    </span>
                                </p>
                                <p><strong>Genre: </strong><asp:Label ID="lblGenre" runat="server" /></p>
                                <p><strong>Language: </strong><asp:Label ID="lblLanguage" runat="server" /></p>
                                <p><strong>Duration: </strong><asp:Label ID="lblDuration" runat="server" /></p>
                                <p><strong>Director: </strong><asp:Label ID="lblDirector" runat="server" /></p>
                                <p><strong>Cast: </strong><asp:Label ID="lblCast" runat="server" /></p>
                                <p><strong>Movie Location: </strong><asp:Label ID="lblLocation" runat="server" /></p>
                                <p><strong>Songs: </strong><asp:Label ID="lblSongs" runat="server" /></p>
                                <p><strong>Description: </strong><asp:Label ID="lblDescription" runat="server" TextMode="MultiLine" /></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
