using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieRecommandation
{
    public partial class EditMovies : System.Web.UI.Page
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
                        txtName.Text = reader["Name"].ToString();
                        txtIMDb.Text = reader["IMDb"].ToString();
                        txtGenre.Text = reader["Genre"].ToString();
                        txtLanguage.Text = reader["Language"].ToString();
                        txtDescription.Text = reader["Discription"].ToString();
                        txtDuration.Text = reader["Duration"].ToString();
                    }

                    conn.Close();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int movieId = Convert.ToInt32(Request.QueryString["Id"]);
            string name = txtName.Text;
            float imdbRating = float.Parse(txtIMDb.Text);

            string genre = txtGenre.Text;
            string language = txtLanguage.Text;
            string description = txtDescription.Text;
            int duration = Convert.ToInt32(txtDuration.Text);

            byte[] posterBytes = null;
            if (filePoster.HasFile)
            {
                posterBytes = filePoster.FileBytes; // Read the uploaded image into byte array
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Movies SET Name=@Name, IMDb=@IMDb, Genre=@Genre, Language=@Language, Discription=@Discription, Duration=@Duration";

                if (posterBytes != null)
                {
                    query += ", Poster=@Poster"; // Include the poster only if a new one was uploaded
                }

                query += " WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@IMDb", imdbRating);
                    cmd.Parameters.AddWithValue("@Genre", genre);
                    cmd.Parameters.AddWithValue("@Language", language);
                    cmd.Parameters.AddWithValue("@Discription", description);
                    cmd.Parameters.AddWithValue("@Duration", duration);

                    if (posterBytes != null)
                    {
                        cmd.Parameters.AddWithValue("@Poster", posterBytes); // Add the new poster if it was uploaded
                    }

                    cmd.Parameters.AddWithValue("@Id", movieId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblMessage.Text = "Movie updated successfully!";
                }
            }
            Response.Redirect("AdminPanel.aspx");
        }
    }
}