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
        private static string[] arroz = { "title_popular", "title_substring", "title_approx" };

        //http://www.imdb.com/xml/find?json=1&tt=on&q=Minions
        private static string BaseURL = "http://www.imdb.com/xml/find?json=1&tt=on&q=";

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
                    var jsonMovie = jobject[arroz[i]];
                    foreach (var jMovie in jsonMovie)
                    {
                        movies.Add(ImdbParser.ParseToMovie(jMovie));
                        Console.WriteLine(movies.Count);
                    }

                }
            }

            return movies;
        }
    }
}
