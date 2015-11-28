using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TTMovieModel.Model;
using TamTamMovie.Models;

namespace TamTamMovie.Controllers
{
    public class HomeController : AsyncController
    {

        private Repository repository = new Repository();

        public ActionResult Index()
        {
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

        public void LoadVideos(string movieID)
        {
            repository.LoadVideo(movieID);
        }



        public async Task<ActionResult> GetMovie(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            YoutubeVideoProvider yt = new YoutubeVideoProvider();
            movie.AddTrailers(yt.GetVideoFor(movie.Title.Name));

            return View("ViewMovieDetailInfo", movie);
        }
    }
}
