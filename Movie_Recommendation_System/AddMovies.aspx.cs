using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Recommendation_System
{
    public partial class AddMovies : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["MRS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            float imdb;
            int duration;

            if (!float.TryParse(txtIMDb.Text.Trim(), out imdb))
            {
                lblMessage.Text = "Invalid IMDb rating. Please enter a valid number.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            imdb = (float)Math.Round(imdb, 1);

            if (!int.TryParse(txtDuration.Text.Trim(), out duration))
            {
                lblMessage.Text = "Invalid Duration. Please enter a valid number.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string genre = txtGenre.Text.Trim();
            string language = txtLanguage.Text.Trim();
            string description = txtDescription.Text.Trim();

            // Fetch poster image data
            byte[] poster = null;
            if (filePoster.HasFile)
            {
                using (BinaryReader br = new BinaryReader(filePoster.PostedFile.InputStream))
                {
                    poster = br.ReadBytes(filePoster.PostedFile.ContentLength);
                }
            }

           
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Movies (Name, IMDb, Genre, Language, Discription, Duration, Poster) " +
                               "VALUES (@Name, @IMDb, @Genre, @Language, @Discription, @Duration, @Poster)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@IMDb", imdb);
                    cmd.Parameters.AddWithValue("@Genre", genre);
                    cmd.Parameters.AddWithValue("@Language", language);
                    cmd.Parameters.AddWithValue("@Discription", description);
                    cmd.Parameters.AddWithValue("@Duration", duration);
                    cmd.Parameters.AddWithValue("@Poster", poster);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch(SqlException ex)
                    {
                        lblMessage.Text = "SQL Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }

                    conn.Close();
                }
            }
            Response.Redirect("AdminPanel.aspx");
        }
    }
}