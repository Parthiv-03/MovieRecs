using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Recommendation_System
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float IMDb { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public byte[] Poster { get; set; }

        public string PosterBase64
        {
            get
            {
                if (Poster != null && Poster.Length > 0)
                {
                    return "data:image/png;base64," + Convert.ToBase64String(Poster);
                }
                return null;
            }
        }
    }
}