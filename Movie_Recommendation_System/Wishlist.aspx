<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="Movie_Recommendation_System.Wishlist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>My Wishlist</title>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container p-5">
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h2>My Wishlist</h2>
                <!-- Back button to navigate back -->
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary btn-back" OnClick="btnBack_Click" />
            </div>
            <asp:Label ID="wishlistError" ForeColor="Red" runat="server"></asp:Label>
            <div class="row">
                <asp:Repeater ID="WishlistRepeater" runat="server">
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
                                    <asp:Button ID="btnDetails" runat="server" Text="Details" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary" OnClick="btnDetails_Click" />
                                    <asp:Button ID="btnRemoveWishlist" runat="server" Text="Remove"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnClick="btnRemoveWishlist_Click" CssClass="btn btn-danger ml-3" />
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
</body>
</html>
