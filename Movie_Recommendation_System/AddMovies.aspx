<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMovies.aspx.cs" Inherits="Movie_Recommendation_System.AddMovies" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Add Movie</title>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form2" runat="server" enctype="multipart/form-data">
        <div class="container">
            <h2 class="mt-4">Add New Movie</h2>
            <div class="form-group">
                <label for="txtName">Movie Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtIMDb">IMDb Rating</label>
                <asp:TextBox ID="txtIMDb" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtGenre">Genre</label>
                <asp:TextBox ID="txtGenre" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtLanguage">Language</label>
                <asp:TextBox ID="txtLanguage" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtDescription">Description</label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
            </div>
            <div class="form-group">
                <label for="txtDuration">Duration (minutes)</label>
                <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="fuPoster">Movie Poster</label>
                <asp:FileUpload ID="filePoster" runat="server" CssClass="form-control-file" />
            </div>
            <asp:Button ID="btnAddMovie" runat="server" CssClass="btn btn-primary" Text="Add Movie" OnClick="btnAddMovie_Click" />
            <!-- Label for displaying messages -->
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-3" />
        </div>
    </form>
</body>
</html>
