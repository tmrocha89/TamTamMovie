using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TTMovieModel.Model;


namespace TamTamMovie.Controllers
{
    public class HomeController : AsyncController
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

          //  ImdbMovieInformation imdb = new ImdbMovieInformation();
           // Task<IList<Movie>> movies = imdb.getAllMovies("Minions");

           // YoutubeVideoProvider youtube = new YoutubeVideoProvider();
           // youtube.Run();
            IList<Movie> movies = new List<Movie>();
            return View(movies);
        }


        public async Task<ActionResult> LoadInformation()
        {

            AsyncManager.OutstandingOperations.Increment();

            ImdbMovieInformation imdb = new ImdbMovieInformation();
            IList<Movie> movies = await imdb.getAllMovies("Minions");

            AsyncManager.OutstandingOperations.Decrement();
            return View("ViewMovieInformation", movies);
        }
        

    }
}
