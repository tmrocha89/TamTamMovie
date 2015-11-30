using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TamTamMovie.Models.ModelView;

namespace TamTamMovie.Models.ClientWeb
{
    public class Repository
    {
        private const string BASE_URL = "http://tamtammovie.azurewebsites.net/"; //this should be dynamically


        private IList<MovieDTO> jsonToDtoMovies(string jsonString)
        {
            IList<MovieDTO> dtoMovies = JsonConvert.DeserializeObject<List<MovieDTO>>(jsonString);
            return dtoMovies;
        }

        private MovieDTO jsonToDtoMovie(string jsonString)
        {
            return JsonConvert.DeserializeObject<MovieDTO>(jsonString);
        }

        //uses application/json
        private async Task<HttpResponseMessage> getHttpResponseMessage(string urlRequest)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                return await client.GetAsync(urlRequest);
            }
        }

        public async Task<IList<MovieDTO>> GetMoviesBasicInformation(string movieName)
        {
            if (!String.IsNullOrWhiteSpace(movieName))
            {
                string req = "api/Movie/" + movieName + "/false";
                HttpResponseMessage response = await getHttpResponseMessage(req);
                if (response.IsSuccessStatusCode)
                {
                    var strDtoMovies = await response.Content.ReadAsAsync<string>();
                    return jsonToDtoMovies(strDtoMovies);
                }
            }
            return (new List<MovieDTO>());
        }

        public async Task<string> GetCoverFor(string movieID)
        {
            string req = "api/Movie/" + movieID + "/true?cover=true";
            HttpResponseMessage response = await getHttpResponseMessage(req);
            if (response.IsSuccessStatusCode)
            {
                var strDtoMovieImage = await response.Content.ReadAsAsync<string>();
                MovieDTO movieDtoImage = jsonToDtoMovie(strDtoMovieImage);
                return movieDtoImage.CoverUrl;
            }
            return "";

        }

        public async Task<MovieDTO> LoadDetailedData(string movieID)
        {
            string req = "api/Movie/" + movieID + "/true?details=true";
            HttpResponseMessage response = await getHttpResponseMessage(req);
            if (response.IsSuccessStatusCode)
            {
                var strDtoMovie = await response.Content.ReadAsAsync<string>();
                MovieDTO movieDto = jsonToDtoMovie(strDtoMovie);
                return movieDto;

            }
            return null;
        }

        //used in Javascript
        public async Task<MovieDTO> GetMovie(string movieID)
        {
            string req = "api/Movie/" + movieID + "/true?details=true";
            HttpResponseMessage response = await getHttpResponseMessage(req);
            if (response.IsSuccessStatusCode)
            {
                var strDtoMovie = await response.Content.ReadAsAsync<string>();
                MovieDTO movieDto = JsonConvert.DeserializeObject<MovieDTO>(strDtoMovie);
                return movieDto;
            }
            return null;

        }
    }
}