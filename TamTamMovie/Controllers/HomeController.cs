using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Diagnostics;
using System.Net.Http;
using TamTamMovie.Models.ModelView;
using Newtonsoft.Json;
using System;

namespace TamTamMovie.Controllers
{
    public class HomeController : AsyncController
    {

        // private Repository repository = new Repository();
        private const string BASE_URL = "http://tamtammovie.azurewebsites.net/";// "http://localhost:51191/";

        public ActionResult Index(string id)
        {
            ViewBag.Title = "Home Page";
            IList<MovieDTO> movies = new List<MovieDTO>();
            return View();
        }


        public async Task<ActionResult> GetMoviesBasicInformation(string movieName)
        {
            if (!String.IsNullOrWhiteSpace(movieName))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(BASE_URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    string req = "api/Movie/" + movieName + "/false";
                    HttpResponseMessage response = await client.GetAsync(req);
                    if (response.IsSuccessStatusCode)
                    {
                        var strDtoMovies = await response.Content.ReadAsAsync<string>();
                        IList<MovieDTO> dtoMovies = JsonConvert.DeserializeObject<List<MovieDTO>>(strDtoMovies);
                        return View("ViewMovieInformation", dtoMovies);
                    }
                }
            }
            return View("ViewMovieInformation", null);
        }

        public async Task<string> LoadCoverFor(string movieID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string req = "api/Movie/"+movieID+"/true?cover=true";
                HttpResponseMessage response = await client.GetAsync(req);
                if (response.IsSuccessStatusCode)
                {
                    var strDtoMovieImage = await response.Content.ReadAsAsync<string>();
                    MovieDTO movieDtoImage = JsonConvert.DeserializeObject<MovieDTO>(strDtoMovieImage);
                    return movieDtoImage.CoverUrl;

                }
                return "";
            }
        }

        public async Task<MovieDTO> LoadDetailedData(string movieID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string req = "api/Movie/" + movieID + "/true?details=true";
                HttpResponseMessage response = await client.GetAsync(req);
                if (response.IsSuccessStatusCode)
                {
                    var strDtoMovie = await response.Content.ReadAsAsync<string>();
                    MovieDTO movieDto = JsonConvert.DeserializeObject<MovieDTO>(strDtoMovie);
                    return movieDto;

                }
                return null;
            }
            //return await repository.LoadDetailedData(movieID);
        }

        //used in Javascript
        public async Task<ActionResult> GetMovie(string movieID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string req = "api/Movie/" + movieID + "/true?details=true";
                HttpResponseMessage response = await client.GetAsync(req);
                if (response.IsSuccessStatusCode)
                {
                    var strDtoMovie = await response.Content.ReadAsAsync<string>();
                    MovieDTO movieDto = JsonConvert.DeserializeObject<MovieDTO>(strDtoMovie);
                    return View("ViewMovieDetailInfo", movieDto);

                }
                return View("ViewMovieDetailInfo", null);
            }
            /*
            Movie movie = MovieCache.getMovie(movieID);
            YoutubeVideoProvider yt = new YoutubeVideoProvider();
            movie.AddTrailers(yt.GetVideoFor(movie.Title.Name));

            return View("ViewMovieDetailInfo", movie);
            */
        }

        /*
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
         */
    }
}
