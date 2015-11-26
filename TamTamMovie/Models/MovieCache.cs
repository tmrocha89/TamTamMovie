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

        public static void AddMovies(IList<Movie> movies)
        {
            foreach(Movie movie in movies)
            {
                HttpContext.Current.Cache[movie.ID] = movie;
            }
        }

        public static IList<Movie> GetAMovies(string movieName)
        {
            IList<Movie> movies = new List<Movie>();
            foreach (DictionaryEntry DEmovie in HttpContext.Current.Cache)
            {
                try {
                    Movie movieTmp = DEmovie.Value as Movie;
                    if (movieTmp.Title.Name.Contains(movieName))
                    {
                        movies.Add(movieTmp);
                    }
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
            foreach(Movie movie in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove(movie.ID);
            }
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