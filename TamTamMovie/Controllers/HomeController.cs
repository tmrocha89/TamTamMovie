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

        private Repository repository = new Repository();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            IList<Movie> movies = new List<Movie>(); // await repository.GetMovies("Minions");
            //ImdbMovieInformation imdb = new ImdbMovieInformation();
            //IList<Movie> movies = await imdb.getAllMovies("Minions");
            //new ImdbMovieInformation().GetAllInformation("tt2293640");
            //YoutubeVideoProvider youtube = new YoutubeVideoProvider();
            //IList<Video> videos = youtube.getVideo("Minions");
            //IList<Movie> movies = new List<Movie>();
            return View();
        }


        public async Task<ActionResult> GetMoviesBasicInformation()
        {
            /*
            AsyncManager.OutstandingOperations.Increment();

            ImdbMovieInformation imdb = new ImdbMovieInformation();
            IList<Movie> movies = await imdb.getAllMovies("Minions");

            //  Cache.["Movies"] = movies;
            MovieCache.AddMovies(movies);
            //Task<IList<Movie>> moviess = LoadVideos(null);
            Task task = Task.Factory.StartNew(() => SearchVideosAsync(movies) );
            AsyncManager.OutstandingOperations.Decrement();
            */
            IList<Movie> movies = await repository.GetMovies("Minions");
            return View("ViewMovieInformation", movies);
        }

        public async Task<string> LoadCoverFor(string movieID)
        {
            Image image = await repository.GetCoverFor(movieID);
            if (image != null)
            {
                return image.Link;
            }
            return null;
        }

        private void LoadVideos()
        {
            repository.LoadVideos();
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
            movie.AddTrailers(yt.GetVideoFor(movie.Title.Name));

            return View("ViewMovieDetailInfo", movie);
        }
             
        public async Task<IList<Movie>> LoadVideos(IList<Movie> movies)
        {
            movies = MovieCache.GetMovies();
            YoutubeVideoProvider yt = new YoutubeVideoProvider();
            foreach( Movie movie in movies)
            {
                movie.AddTrailers(yt.GetVideoFor(movie.Title.Name));
            }
            return movies;
        }
        

    }
}
