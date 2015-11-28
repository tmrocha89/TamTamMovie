using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TTMovieModel.DAL;
using TTMovieModel.Model;

namespace TamTamMovie.Models
{
    public class Repository
    {
        private DataAccess dataAccess;

        public Repository()
        {
            dataAccess = new DataAccess();
        }


        public async Task<IList<Movie>> GetMovies(string movieName)
        {
            Clear();
            IList<Movie> movies = null;
            movies = MovieCache.GetMovies();
            if(movies == null || movies.Count == 0)
            {
                movies = await dataAccess.GetMovies(movieName);
                MovieCache.AddMovies(movies);
            }

            return movies;
        }

        public void LoadVideos()
        {
            IList<Movie> movies = MovieCache.GetMovies();
            foreach(Movie mov in movies)
            {
                IList<Video> videos = dataAccess.GetVideoFor(mov.ID);
            }
        }

        public async Task<Image> GetCoverFor(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            if (movie != null && movie.hasCover())
            {
                return movie.Cover;
            }
            Image image = await dataAccess.GetCoverFor(movieID);
            if(image != null)
            {
                movie.Cover = image;
                MovieCache.UpdateMovie(movie);
            }
            return image;
        }

        public void Clear()
        {
            MovieCache.Clear();
        }

        public async Task<Movie> LoadDetailedData(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            if (movie != null)
            {
                movie = await dataAccess.GetInformationFor(movie);
                movie.Trailers = dataAccess.GetVideoFor(movie.Title.Name);
                MovieCache.UpdateMovie(movie);
                return movie;
            }
            return null;
        }
    }
}