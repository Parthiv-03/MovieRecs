using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Recommendation_System
{
    public partial class MovieDetails : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["MRS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int movieId = Convert.ToInt32(Request.QueryString["Id"]);
                LoadMovieDetails(movieId);
            }
        }

        // Load movie details from the database
        private void LoadMovieDetails(int movieId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Movies WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", movieId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblMovieName.Text = reader["Name"].ToString();
                        lblIMDb.Text = reader["IMDb"].ToString();
                        lblGenre.Text = reader["Genre"].ToString();
                        lblLanguage.Text = reader["Language"].ToString();

                        int durationInMinutes = Convert.ToInt32(reader["Duration"]);
                        TimeSpan time = TimeSpan.FromMinutes(durationInMinutes);
                        lblDuration.Text = $"{(int)time.TotalHours}h {time.Minutes}m";

                        lblDirector.Text = reader["Director"].ToString();
                        lblCast.Text = reader["Cast"].ToString();
                        lblLocation.Text = reader["MovieLocation"].ToString();
                        lblSongs.Text = reader["Songs"].ToString();
                        lblDescription.Text = reader["Discription"].ToString();

                        // Convert byte array (poster) to base64 string for displaying
                        byte[] posterBytes = (byte[])reader["Poster"];
                        string base64Poster = Convert.ToBase64String(posterBytes);
                        imgPoster.ImageUrl = "data:image/png;base64," + base64Poster;
                    }

                    conn.Close();
                }
            }
        }
    }
}