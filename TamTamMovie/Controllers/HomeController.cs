using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TTMovieModel.Model;
using System.Web.Caching;
using TamTamMovie.Models;

namespace TamTamMovie.Controllers
{
    public class HomeController : AsyncController
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

          //  ImdbMovieInformation imdb = new ImdbMovieInformation();
           // Task<IList<Movie>> movies = imdb.getAllMovies("Minions");

            YoutubeVideoProvider youtube = new YoutubeVideoProvider();
            Video video = youtube.getVideo("Minions");
            IList<Movie> movies = new List<Movie>();
            return View(movies);
        }


        public async Task<ActionResult> LoadInformation()
        {

            AsyncManager.OutstandingOperations.Increment();

            ImdbMovieInformation imdb = new ImdbMovieInformation();
            IList<Movie> movies = await imdb.getAllMovies("Minions");

            //  Cache.["Movies"] = movies;
            MovieCache.AddMovies(movies);
            //Task<IList<Movie>> moviess = LoadVideos(null);
            Task task = Task.Factory.StartNew(() => SearchVideosAsync(movies) );
            AsyncManager.OutstandingOperations.Decrement();
            return View("ViewMovieInformation", movies);
        }

        private async Task<ActionResult> SearchVideosAsync(IList<Movie> movies)
        {
            //AsyncManager.OutstandingOperations.Increment();
            movies = await LoadVideos(movies);
            return View(movies);
            //AsyncManager.OutstandingOperations.Decrement();
        }




        public async Task<ActionResult> GetMovie(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            YoutubeVideoProvider yt = new YoutubeVideoProvider();
            movie.Trailer = yt.getVideo(movie.Title.Name);

            return View("ViewMovieDetailInfo", movie);
        }
             
        public async Task<IList<Movie>> LoadVideos(IList<Movie> movies)
        {
            movies = MovieCache.GetAMovies("Minions");
            YoutubeVideoProvider yt = new YoutubeVideoProvider();
            foreach( Movie movie in movies)
            {
                movie.Trailer = yt.getVideo(movie.Title.Name);
            }
            return movies;
        }
        

    }
}
