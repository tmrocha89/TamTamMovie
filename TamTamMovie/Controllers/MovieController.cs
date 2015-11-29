using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TamTamMovie.Models;
using TTMovieModel.Model;

namespace TamTamMovie.Controllers
{
    public class MovieController : ApiController
    {
        private Repository repository = new Repository();


        // GET: api/Movie/5
        public async Task<string> Get(string movieName,
            string movieID = "",
            bool? details=false,
            bool? cover=false)
        {
            if (!String.IsNullOrWhiteSpace(movieName))
            {
                
                if (details == true)
                {
                    return await repository.GetMoviesToSend(movieName, true);
                }
                else
                {
                    // guardo o objeto com a informacao completa
                }

            }
            return "";
            //IList<Movie> movies = await GetMoviesBasicInformation();
            //valor = repository.getMovieToSend(movies);
        }


        private async Task<IList<Movie>> GetMoviesBasicInformation()
        {
            return await repository.GetMovies("Minions");
        }

        private async Task<string> LoadCoverFor(string movieID)
        {
            Image image = await repository.GetCoverFor(movieID);
            if (image != null)
            {
                return image.Link;
            }
            return null;
        }

        private async Task<Movie> LoadDetailedData(string movieID)
        {
            return await repository.LoadDetailedData(movieID);
        }

        //used in Javascript
        private async Task<string> GetMovie(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            YoutubeVideoProvider yt = new YoutubeVideoProvider();
            movie.AddTrailers(yt.GetVideoFor(movie.Title.Name));

            //return View("ViewMovieDetailInfo", movie);
            return null;
        }


    }
}
