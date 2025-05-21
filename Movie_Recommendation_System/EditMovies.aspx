<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMovies.aspx.cs" Inherits="MovieRecommandation.EditMovies" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Movie</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>Edit Movie</h2>
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
                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtDuration">Duration (in minutes)</label>
                <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="filePoster">Movie Poster</label>
                <asp:FileUpload ID="filePoster" runat="server" CssClass="form-control-file" />
            </div>
            <asp:Button ID="btnUpdate" runat="server" Text="Update Movie" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-success" />
        </div>
    </form>
</body>
</html>

