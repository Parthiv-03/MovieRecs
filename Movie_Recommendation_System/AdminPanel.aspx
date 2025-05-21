<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Movie_Recommendation_System.AdminPanel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Admin Panel - Movies</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 20px 0;
        }
        .header h2 {
            margin: 0;
            color: #343a40;
        }
        .card-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: flex-start;
            gap: 20px;
            margin-top: 20px;
        }
        .card {
            width: 18rem;
            background-color: #ffffff;
            border: 1px solid #ddd;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .card img {
            height: 300px;
            object-fit: cover;
        }
        .action-buttons {
            margin-top: 10px;
        }
        .btnAddMovie {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            color: #fff;
            background-color: #28a745;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btnAddMovie:hover {
            background-color: #218838;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h2>Admin Panel - Movies</h2>
                <div>
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary btn-back" Text="Back to Home" OnClick="btnBack_Click" />
                <!-- Add New Movie Button positioned at the top-right -->
                <asp:Button ID="btnAddMovie" runat="server" CssClass="btnAddMovie" Text="Add New Movie" OnClick="btnAddMovie_Click" />
                </div>
            </div>

            <!-- Repeater for displaying movies as cards -->
            <div class="card-container">
                <asp:Repeater ID="rptMovies" runat="server" OnItemDataBound="MoviesRepeater_SetAttribute">
                    <ItemTemplate>
                        <div class="card">
                            <asp:Image ID="imgPoster" runat="server" CssClass="card-img-top" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("Poster")) %>' />
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Name") %></h5>
                                <p class="card-text">
                                    IMDb: <%# Eval("IMDb") %><br />
                                    Genre: <%# Eval("Genre") %><br />
                                </p>
                                 <div class="action-buttons">
                                    <asp:Button ID="btnDetails" runat="server" Text="Details" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary" OnClick="btnDetails_Click" />
                                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>' Text="Edit" CssClass="btn btn-warning" Onclick="btnEdit_Click"/>
                                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
