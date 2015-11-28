using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class ImdbMovieInformation : IMovieInformation
    {
        private static string[] arrayNames = { "title_popular", "title_substring", "title_approx" };

        private const string BASE_URL = "http://www.imdb.com/xml/find?json=1&tt=on&q=";
        private const string BEGIN_DESCRIPTION_URL = "http://www.omdbapi.com/?i=";
        private const string END_DESCRITPION_URL = "&plot=full&r=json";
        private ImdbParser parser;

        public ImdbMovieInformation()
        {
            parser = new ImdbParser();
        }

        public async Task<IList<Movie>> getAllMovies(string name)
        {
            IList<Movie> movies = new List<Movie>();
            string url = BASE_URL + name;
            
            using (WebClient webClient = new WebClient())
            {               
                var jsonText = await webClient.DownloadStringTaskAsync(url);
                Debug.WriteLine("My debug string here");
                JObject jobject = JObject.Parse(jsonText);
                for (int i = 0; i < jobject.Count; i++)
                {
                    var jsonMovie = jobject[arrayNames[i]];
                    foreach (var jMovie in jsonMovie)
                    {
                        Movie movieTmp = parser.GetBasicMovieInformation(jMovie);
                        movies.Add(movieTmp);
                        //movies.Add( await getFullDescriptionFor(movieTmp));
                        Debug.WriteLine(movies.Count);
                    }

                }
            }

            return movies;
        }

        public async Task<Movie> getFullDescriptionFor(Movie movie)
        {
            string url = BEGIN_DESCRIPTION_URL + movie.ID + END_DESCRITPION_URL;
            using (WebClient webClient = new WebClient())
            {
                var jsonText = await webClient.DownloadStringTaskAsync(url);

                JObject movieJson = JObject.Parse(jsonText);

                return parser.GetDetailInformation(movieJson, movie);
                
            }

        }

        public async Task<Image> GetCoverFor(string movieID)
        {
            string url = BEGIN_DESCRIPTION_URL + movieID + END_DESCRITPION_URL;
            using (WebClient webClient = new WebClient())
            {
                try {
                    var jsonText = await webClient.DownloadStringTaskAsync(url);

                    JObject movieJson = JObject.Parse(jsonText);

                    return parser.GetCover(movieJson);
                }catch(WebException ex)
                {
                    Debug.WriteLine("Link: "+url+"\n"+ex.Message);
                    return null;
                }

            }
        }

        public async Task<Movie> GetInformationFor(Movie movie)
        {
            
            string url = BEGIN_DESCRIPTION_URL + movie.ID + END_DESCRITPION_URL;
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    var jsonText = webClient.DownloadString(url);

                    JObject movieJson = JObject.Parse(jsonText);

                    return parser.GetDetailInformation(movieJson, movie);
                }
                catch (WebException ex)
                {
                    Debug.WriteLine("Link: " + url + "\n" + ex.Message);
                    return movie;
                }

            }
            
        }



        /*
                public async Task<IList<Movie>> getAllMovies(string name)
        {
            IList<Movie> movies = new List<Movie>();
            string url = BaseURL + name;
            
            using (WebClient webClient = new WebClient())
            {               
                var jsonText = await webClient.DownloadStringTaskAsync(url);
                Debug.WriteLine("My debug string here");
                JObject jobject = JObject.Parse(jsonText);
                for (int i = 0; i < jobject.Count; i++)
                {
                    var jsonMovie = jobject[arrayNames[i]];
                    foreach (var jMovie in jsonMovie)
                    {
                        movies.Add(ImdbParser.GetBasicMovieInformation(jMovie));
                        Console.WriteLine(movies.Count);
                    }

                }
            }

            return movies;
        }
        */
        /*
                private HtmlDocument getHtmlDocument(Movie movie)
                {
                    return (new HtmlWeb()).Load("http://www.imdb.com/title/" + movie.ID);
                }

                private Movie getMovieWithImage(Movie movie)
                {
                    return parser.GetMovieWithImage(getHtmlDocument(movie), movie);
                }

                private Movie getAllInformation(Movie movie)
                {
                    movie = parser.GetDetailInformation( getHtmlDocument(movie) , movie);
                    return movie;
                }

                */
    }
}
