using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Caching;
using TTMovieModel.Model;

namespace TamTamMovie.Models
{
    public static class MovieCache
    {

        public static void AddMovies(string movieName, IList<Movie> movies)
        {
            Clear(); // Clear cache to store only the items that are display
            HttpContext.Current.Cache["movieName"] = movieName;
            foreach (Movie movie in movies)
            {
                HttpContext.Current.Cache[movie.ID] = movie;
            }
        }

        public static IList<Movie> GetMovies(string movieName)
        {
            if (movieName != null && movieName != (string)HttpContext.Current.Cache["movieName"])
                return null;
            IList<Movie> movies = new List<Movie>();
            foreach (DictionaryEntry DEmovie in HttpContext.Current.Cache)
            {
                try {
                    Movie movieTmp = DEmovie.Value as Movie;
                    // if (movieTmp.Title.Name.Contains(movieName))
                    // {
                    if (movieTmp != null)
                    {
                        movies.Add(movieTmp);
                    }
                   // }
                }catch(NullReferenceException)
                {
                    // DEmovie is not a movie
                    // best luck in next try :P
                    Debug.Write(DEmovie);
                }
            }
            return movies;
        }

        public static void Clear()
        {
            try {
                foreach (Movie movie in HttpContext.Current.Cache)
                {
                    HttpContext.Current.Cache.Remove(movie.ID);
                }
            }
            catch (InvalidCastException)
            {
                //not a movie
            }
        }

        public static void UpdateMovie(Movie movie)
        {
            HttpContext.Current.Cache[movie.ID] = movie;
        }

        public static Movie getMovie(string movieID)
        {
            return HttpContext.Current.Cache[movieID] as Movie;
        }

        public static void Remove(Movie movie)
        {
            HttpContext.Current.Cache.Remove(movie.ID);
        }
    }
}