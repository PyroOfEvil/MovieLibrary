using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary
{
    public class MovieFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        
        public string FilePath { get; set; }
        public List<Movie> Movies { get; set; }

        
        public MovieFile(string path)
        {
            Movies = new List<Movie>();
            FilePath = path;
            
            try
            {
                StreamReader sr = new StreamReader(FilePath);
                
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    
                    Movie movie = new Movie();
                    string line = sr.ReadLine();
                    
                    int index = line.IndexOf('"');
                    if (index == -1)
                    {
                        
                        string[] movieDetails = line.Split(',');
                        movie.movieId = Int64.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                    }
                    else
                    {
                        
                        movie.movieId = Int64.Parse(line.Substring(0, index - 1));
                        
                        line = line.Substring(index + 1);
                        
                        index = line.IndexOf('"');
                        
                        movie.title = line.Substring(0, index);
                        
                        line = line.Substring(index + 2);
                        
                        movie.genres = line.Split('|').ToList();
                    }
                    Movies.Add(movie);
                }
                
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        
        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddMovie(Movie movie)
        {
            try
            {

                movie.movieId = Movies.Max(m => m.movieId) + 1;

                string Title = movie.title.IndexOf(',') != -1 ? $"\"{movie.title}\"" : movie.title;
                StreamWriter sw = new StreamWriter(FilePath, true);
                sw.WriteLine($"{movie.movieId},{Title},{string.Join("|", movie.genres)}");
                sw.Close();
                
                Movies.Add(movie);
                
                logger.Info("Movie id {Id} added", movie.movieId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}