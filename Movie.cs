using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class Movie
    {
        
        public Int64 movieId { get; set; }
        public string title { get; set; }
        public List<string> genres { get; set; }

        
        public Movie()
        {
            genres = new List<string>();
        }

        
        public string Display()
        {
            return $"Id: {movieId} Title: {title} Genres: {string.Join(", ", genres)}";
        }
    }
}