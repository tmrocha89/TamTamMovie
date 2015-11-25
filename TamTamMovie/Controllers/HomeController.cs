using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TTMovieModel.Model;

namespace TamTamMovie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ImdbMovieInformation imdb = new ImdbMovieInformation();
            IList<Movie> movies = imdb.getAllMovies("Minions");
            return View(movies);
        }
    }
}
