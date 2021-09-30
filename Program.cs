using System;
using System.Collections.Generic;
using NLog;

namespace MovieLibrary
{
    class MainClass
    {
        
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            string file =  @"C:\Users\PyroOfEvil\RiderProjects\MovieLibrary\MovieLibrary\movies.csv";
            
            logger.Info("Program started");

            MovieFile movieFile = new MovieFile(file);
            string choice = "";
            do
            {
                
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("Enter to quit");
                
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                if (choice == "1")
                {
                    
                    Movie movie = new Movie();
                    
                    Console.WriteLine("Enter movie title");
                    
                    movie.title = Console.ReadLine();
                    
                    if (movieFile.isUniqueTitle(movie.title)){
                        
                        string input;
                        do
                        {
                            
                            Console.WriteLine("Enter genre (or done to quit)");
                            
                            input = Console.ReadLine();
                            
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        
                        movieFile.AddMovie(movie);
                    }
                    else
                    {
                        Console.WriteLine("Movie title already exists\n");
                    }

                } else if (choice == "2")
                {
                    
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
            } while (choice == "1" || choice == "2");

            logger.Info("Program ended");
        }
    }
}