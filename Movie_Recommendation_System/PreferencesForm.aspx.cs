using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Recommendation_System
{
    public partial class PreferencesForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve selected values from CheckBoxList and DropDownList controls
            string[] genres = cblGenre.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .Select(li => li.Text)
                .ToArray();

            string[] languages = cblLanguage.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .Select(li => li.Text)
                .ToArray();

            string rating = ddlRating.SelectedItem != null ? ddlRating.SelectedItem.Text : string.Empty;

            // Store preferences in session variables
            Session["PreferredGenres"] = genres;
            Session["PreferredLanguages"] = languages;
            Session["PreferredRating"] = rating;

            Response.Redirect("Home.aspx");
        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}