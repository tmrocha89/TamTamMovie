using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TTMovieModel.Model;
using TamTamMovie.Models;
using System.Diagnostics;

namespace TamTamMovie.Controllers
{
    public class HomeController : AsyncController
    {

        private Repository repository = new Repository();

        public ActionResult Index(string id)
        {
            if (id != null)
                Debug.WriteLine(id);
            ViewBag.Title = "Home Page";
            IList<Movie> movies = new List<Movie>();
            return View();
        }


        public async Task<ActionResult> GetMoviesBasicInformation()
        {
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

        public async Task<Movie> LoadDetailedData(string movieID)
        {
            return await repository.LoadDetailedData(movieID);
        }

        //used in Javascript
        public async Task<ActionResult> GetMovie(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            YoutubeVideoProvider yt = new YoutubeVideoProvider();
            movie.AddTrailers(yt.GetVideoFor(movie.Title.Name));

            return View("ViewMovieDetailInfo", movie);
        }

    }
}
