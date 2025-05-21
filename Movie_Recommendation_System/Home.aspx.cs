using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie_Recommendation_System
{
    public partial class Home : System.Web.UI.Page
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["MRS"].ConnectionString;
        private List<Movie> movies;
        private List<Movie> _recommended_movies;
        private Dictionary<string, int> genreClickCount;

        private int _userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    _userId = Convert.ToInt32(Session["userId"]);
                }
                else
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }

                if (!Convert.ToBoolean(Session["isAdmin"]))
                {
                    adminPanel.Visible = false;
                }

            }
            genreClickCount = new Dictionary<string, int>();
            movies = new List<Movie>();
            _recommended_movies = new List<Movie>();
            InitializeGenreClickCount();
            Initmovies();
            BindRecommendedMovieRepeater();
        }


        // Initial All movies Binding Logic
        private void Initmovies()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, IMDb, Genre, Language AS Language, Discription AS Description, Duration, Poster FROM Movies";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Movie movie = new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),  // Safe conversion to int
                            Name = reader["Name"].ToString(),
                            IMDb = Convert.ToSingle(reader["IMDb"]),  // Safe conversion to float
                            Genre = reader["Genre"].ToString(),
                            Language = reader["Language"].ToString(),
                            Description = reader["Description"].ToString(),
                            Duration = Convert.ToInt32(reader["Duration"]),  // Safe conversion to int
                            Poster = reader["Poster"] != DBNull.Value ? (byte[])reader["Poster"] : null  // Handle null for byte array
                        };

                        movies.Add(movie);
                    }
                }
            }
            BindAllMovieRepeater();

        }

        // Init Recommended Movies Logic
        private void InitializeGenreClickCount()
        {
            // Retrieve the genre click count from the database

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Genre, ClickCount FROM UserGenreClicks WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", _userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string genre = reader["Genre"].ToString();
                        int clickCount = Convert.ToInt32(reader["ClickCount"]);
                        genreClickCount[genre] = clickCount;
                    }
                }
            }

            if (Session["GenreClickCount"] == null) Session["GenreClickCount"] = genreClickCount;
        }

        //Data Binding Logic
        private void BindAllMovieRepeater()
        {
            MoviesRepeater.DataSource = movies;
            MoviesRepeater.DataBind();
        }
        private void BindRecommendedMovieRepeater()
        {
            RecoMoviesRepeater.DataSource = GetRecommendedMovies();
            RecoMoviesRepeater.DataBind();

            if (_recommended_movies.Count == 0)
            {
                recoerrormsg.Text = "RECOMMENDATION NOT FOUND...";
            }
        }

        
        // Tracking Users
        protected void TrackGenreClick(string genre)
        {
            // Retrieve or initialize the genre click count dictionary
            genreClickCount = Session["GenreClickCount"] as Dictionary<string, int>;

            // Increment the click count for the selected genre
            if (genreClickCount.ContainsKey(genre))
            {
                genreClickCount[genre]++;
            }
            else
            {
                genreClickCount[genre] = 1;
            }

            Session["GenreClickCount"] = genreClickCount;

        }

        //Get Recommendations 
        protected List<Movie> GetRecommendedMovies()
        {
            // Retrieve the genre click count from the session
            genreClickCount = Session["GenreClickCount"] as Dictionary<string, int>;

            if (genreClickCount == null || genreClickCount.Count == 0)
            {
                return _recommended_movies; // No recommendations if no genre clicks
            }

            // Get the top 3 most clicked genres
            var topGenres = genreClickCount.OrderByDescending(g => g.Value)
                                           .Take(3)
                                           .Select(g => g.Key)
                                           .ToList();

            // If there are fewer than 3 genres, adjust the query accordingly
            if (topGenres.Count == 0)
            {
                return _recommended_movies;
            }

            // Shuffle movies from the top 3 genres and recommend them
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, IMDb, Genre, Language, Discription, Duration, Poster FROM Movies WHERE Genre IN (";
                for (int i = 0; i < topGenres.Count; i++)
                {
                    query += $"@Genre{i},";
                }
                query = query.TrimEnd(',') + ")";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Dynamically add parameters for each genre
                    for (int i = 0; i < topGenres.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@Genre{i}", topGenres[i]);
                    }

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    int counter = 8;
                    while (reader.Read() && counter>0)
                    {
                        Movie movie = new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            IMDb = Convert.ToSingle(reader["IMDb"]),
                            Genre = reader["Genre"].ToString(),
                            Language = reader["Language"].ToString(),
                            Description = reader["Discription"].ToString(),
                            Duration = Convert.ToInt32(reader["Duration"]),
                            Poster = reader["Poster"] != DBNull.Value ? (byte[])reader["Poster"] : null
                        };

                        counter--;
                        _recommended_movies.Add(movie);
                    }
                }
            }

            // Shuffle the recommended movies list before returning
            Random rnd = new Random();
            _recommended_movies = _recommended_movies.OrderBy(x => rnd.Next()).ToList();

            return _recommended_movies;
        }





        // Event handler for the Details button
        protected void btnDetails_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string[] args = button.CommandArgument.Split('|');
            int movieId = Convert.ToInt32(args[0]);
            string genre = args[1];

            TrackGenreClick(genre);
            BindRecommendedMovieRepeater();
            Response.Redirect($"MovieDetails.aspx?Id={movieId}");

        }



        // Search And Logout Logic
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSearchMovies();
        }

        private void BindSearchMovies()
        {
            // Simulate getting popular movies
            var allMovies = movies;

            // Apply filters
            string searchName = txtSearchName.Text.Trim().ToLower();
            string selectedGenre = ddlSearchGenre.SelectedValue;
            string selectedLanguage = ddlSearchLanguage.SelectedValue;

            if (searchName == "" && selectedGenre == "-1" && selectedLanguage == "-1")
            {
                txtSearchName.Focus();
                searcherror.Text = "Please Enter the criteria";
                searchRepeater.DataSource = null;
                searchRepeater.DataBind();
                return;
            }

            var filteredMovies = allMovies.Where(m =>
                (string.IsNullOrEmpty(searchName) || m.Name.ToLower().Contains(searchName)) &&
                (selectedGenre == "-1" || m.Genre == selectedGenre) &&
                (selectedLanguage == "-1" || m.Language == selectedLanguage)
            ).ToList();

            if (filteredMovies.Count == 0)
            {
                searcherror.Text = "No movies found matching your search criteria.";
                searchRepeater.DataSource = null; // Clear the previous data
                searchRepeater.DataBind();
            }
            else
            {
                searcherror.Text = "";
                // Bind to Repeater
                searchRepeater.DataSource = filteredMovies;
                searchRepeater.DataBind();
            }
        }

      
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            UpdateGenreClickCountInDatabase();
            // For example, clear authentication and redirect to login page
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx",true);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void UpdateGenreClickCountInDatabase()
        {
            // Retrieve the genre click count from the session
            Dictionary<string, int> genreClickCount = Session["GenreClickCount"] as Dictionary<string, int>;

            if (genreClickCount == null || genreClickCount.Count == 0)
            {
                return; // No genre click counts to update
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var genreCount in genreClickCount)
                {
                    string genre = genreCount.Key;
                    int clickCount = genreCount.Value;
                    _userId = Convert.ToInt32(Session["userId"]);

                    // Check if the genre exists in the database
                    string checkQuery = "SELECT COUNT(*) FROM UserGenreClicks WHERE UserId = @UserId AND Genre = @Genre";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@UserId", _userId);
                        checkCmd.Parameters.AddWithValue("@Genre", genre);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Genre exists, update the click count
                            string updateQuery = "UPDATE UserGenreClicks SET ClickCount = @ClickCount WHERE UserId = @UserId AND Genre = @Genre";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@ClickCount", clickCount);
                                updateCmd.Parameters.AddWithValue("@UserId", _userId);
                                updateCmd.Parameters.AddWithValue("@Genre", genre);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Genre does not exist, insert a new record
                            string insertQuery = "INSERT INTO UserGenreClicks (UserId, Genre, ClickCount) VALUES (@UserId, @Genre, @ClickCount)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@UserId", _userId);
                                insertCmd.Parameters.AddWithValue("@Genre", genre);
                                insertCmd.Parameters.AddWithValue("@ClickCount", clickCount);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
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

        protected void lnkWishlist_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["userId"] == null)
            {
                // Redirect to login if not logged in
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Get the UserId from the session (Assuming it's stored after login)
                int userId = Convert.ToInt32(Session["userId"]);

                // Get the MovieId from the CommandArgument of the button
                Button btn = (Button)sender;
                int movieId = Convert.ToInt32(btn.CommandArgument);


                // Insert movie into Wishlist for the logged-in user
                string query = "INSERT INTO Wishlist (UserId, MovieId) VALUES (@UserId, @MovieId)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@MovieId", movieId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Optionally, display a message to the user
                Response.Write("<script>alert('Movie added to wishlist');</script>");
            }
        }
        protected void btnWishlist_Click(object sender, EventArgs e)
        {
            // Redirect to Wishlist.aspx where the user's wishlist movies will be shown
            Response.Redirect("Wishlist.aspx");
        }

    }
}



