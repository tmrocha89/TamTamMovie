using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http;
using TamTamMovie.Models.ModelView;
using Newtonsoft.Json;
using System;
using TamTamMovie.Models.ClientWeb;

namespace TamTamMovie.Controllers
{
    public class HomeController : AsyncController
    {

        private Repository repository = new Repository(); //from clientWeb

        public ActionResult Index(string id)
        {
            ViewBag.Title = "Home Page";
            return View();
        }


        public async Task<ActionResult> GetMoviesBasicInformation(string movieName)
        {
            return View("ViewMovieInformation", repository.GetMoviesBasicInformation(movieName));
        }

        public async Task<string> LoadCoverFor(string movieID)
        {
            return await repository.GetCoverFor(movieID);
        }

        public async Task<MovieDTO> LoadDetailedData(string movieID)
        {
            return await repository.LoadDetailedData(movieID);
        }

        //used in Javascript
        public async Task<ActionResult> GetMovie(string movieID)
        {
                return View("ViewMovieDetailInfo", repository.GetMovie(movieID));
            }
        }
    }
}
