using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Recommendation_System
{
    public partial class Wishlist : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["MRS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ensure the user is logged in before showing the wishlist
                if (Session["userId"] != null)
                {
                    BindWishlistMovies();
                }
                else
                {
                    wishlistError.Text = "Please log in to view your wishlist.";
                    WishlistRepeater.Visible = false;
                }
            }
        }
        private void BindWishlistMovies()
        {
            DataTable wishlistMovies = GetUserWishlistMovies();

            if (wishlistMovies != null && wishlistMovies.Rows.Count > 0)
            {
                WishlistRepeater.DataSource = wishlistMovies;
                WishlistRepeater.DataBind();
            }
            else
            {
                wishlistError.Text = "Your wishlist is empty.";
                WishlistRepeater.Visible = false;
            }
        }
        private DataTable GetUserWishlistMovies()
        {
            DataTable dtWishlistMovies = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = @"
                        SELECT M.Id, M.Name, M.IMDb, M.Genre, M.Poster
                        FROM Wishlist W
                        INNER JOIN Movies M ON W.MovieId = M.Id
                        WHERE W.UserId = @UserId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use the current user's session ID to get the wishlist
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtWishlistMovies);
                    }
                }
                catch (Exception ex)
                {
                    wishlistError.Text = "An error occurred while fetching the wishlist. Please try again later.";
                    // Log the exception details (optional)
                }
            }

            return dtWishlistMovies;
        }
        protected void btnDetails_Click(object sender, EventArgs e)
        {
            string movieId = ((Button)sender).CommandArgument;
            Response.Redirect("MovieDetails.aspx?Id=" + movieId);
        }
        protected void btnRemoveWishlist_Click(object sender, EventArgs e)
        {
            string movieId = ((Button)sender).CommandArgument;

            if (Session["userId"] != null)
            {
                int userId = Convert.ToInt32(Session["userId"]);
                RemoveMovieFromWishlist(userId, Convert.ToInt32(movieId));
                BindWishlistMovies(); // Rebind the wishlist to reflect the changes
            }
            else
            {
                wishlistError.Text = "Please log in to remove movies from your wishlist.";
            }
        }
        private void RemoveMovieFromWishlist(int userId, int movieId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "DELETE FROM Wishlist WHERE UserId = @UserId AND MovieId = @MovieId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@MovieId", movieId);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    wishlistError.Text = "An error occurred while removing the movie from your wishlist.";
                    // Log the exception details (optional)
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to the MoviesList.aspx or any previous page
            Response.Redirect("Home.aspx");
        }
    }
}