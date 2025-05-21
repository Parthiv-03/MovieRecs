<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreferencesForm.aspx.cs" Inherits="Movie_Recommendation_System.PreferencesForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movie Preferences</title>
    <link rel="stylesheet" type="text/css" href="Content/styles/preferences.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Movie Preferences</h2>

            <asp:Label ID="lblGenre" runat="server" Text="Preferred Genres:" />
            <asp:CheckBoxList ID="cblGenre" runat="server">
                <asp:ListItem Text="Action" Value="Action" />
                <asp:ListItem Text="Comedy" Value="Comedy" />
                <asp:ListItem Text="Drama" Value="Drama" />
                <asp:ListItem Text="Horror" Value="Horror" />
            </asp:CheckBoxList>

            <asp:Label ID="lblLanguage" runat="server" Text="Preferred Languages:" />
            <asp:CheckBoxList ID="cblLanguage" runat="server">
                <asp:ListItem Text="English" Value="English" />
                <asp:ListItem Text="Gujarati" Value="Gujarati" />
                <asp:ListItem Text="Spanish" Value="Spanish" />
                <asp:ListItem Text="French" Value="French" />
                <asp:ListItem Text="German" Value="German" />
            </asp:CheckBoxList>

            <asp:Label ID="lblRating" runat="server" Text="Preferred Rating:" />
            <asp:DropDownList ID="ddlRating" runat="server">
                <asp:ListItem Text="7.5+" Value="7.5" />
                <asp:ListItem Text="8+" Value="8" />
                <asp:ListItem Text="8.5+" Value="8.5" />
                <asp:ListItem Text="9+" Value="9" />
                <asp:ListItem Text="9.5+" Value="9.5" />
            </asp:DropDownList>

            <div class="button-container">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
                <asp:Button ID="btnSkip" runat="server" Text="Skip For Now" OnClick="btnSkip_Click" CssClass="btn" />
            </div>
        </div>
    </form>
</body>
</html>
