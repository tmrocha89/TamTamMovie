using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using TTMovieModel.Model;

namespace TamTamMovie.Models
{
    public static class MovieCache
    {

        public static void AddMovies(IList<Movie> movies)
        {
            foreach(Movie movie in movies)
            {
                HttpContext.Current.Cache.Insert(movie.ID, movie, null, DateTime.Now, Cache.NoSlidingExpiration);
            }
           // HttpContext.Current.Cache.Insert("myStringKey", "my ObjectValue", null,
           //     DateTime.Now, Cache.NoSlidingExpiration);
        }

        public static IList<Movie> GetAllMovies()
        {
            IList<Movie> movies = new List<Movie>();

            foreach (Movie movie in HttpContext.Current.Cache)
            {
                movies.Add(movie);
            }

            return movies;
        }

        public static void Clear()
        {
            foreach(Movie movie in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove(movie.ID);
            }
        }

        public static void Remove(Movie movie)
        {
            HttpContext.Current.Cache.Remove(movie.ID);
        }
    }
}