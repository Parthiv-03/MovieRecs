using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace Movie_Recommendation_System
{

    public partial class AdminPanel : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["MRS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMovies();
            }
        }

        // Load all movies into the Repeater
        private void LoadMovies()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Movies";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptMovies.DataSource = dt;
                rptMovies.DataBind();
            }
        }

        protected void MoviesRepeater_SetAttribute(object sender, RepeaterItemEventArgs e)
        {
            // Only proceed for actual items (not header/footer items)
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Find the Image control within the current Repeater item
                Image imgPoster = (Image)e.Item.FindControl("imgPoster");

                if (imgPoster != null)
                {
                    // Add the lazy loading attribute
                    imgPoster.Attributes["loading"] = "lazy";
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int movieId = Convert.ToInt32(btnDelete.CommandArgument);

            Response.Redirect("EditMovies.aspx?id=" + movieId);
        }


        // Handle delete button click event
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int movieId = Convert.ToInt32(btnDelete.CommandArgument);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Movies WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", movieId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            LoadMovies(); // Refresh the movie list after deletion
        }

        // Add New Movie button click event
        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
            // Redirect to AddMovie.aspx page to add a new movie
            Response.Redirect("AddMovies.aspx");
        }
        protected void btnDetails_Click(object sender, EventArgs e)
        {
            string movieId = ((Button)sender).CommandArgument;
            Response.Redirect("MovieDetails.aspx?Id=" + movieId);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to the MoviesList.aspx or any previous page
            Response.Redirect("Home.aspx");
        }
    }
}