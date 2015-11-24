using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class ImdbMovieInformation : IMovieInformation
    {
        //http://www.imdb.com/xml/find?json=1&tt=on&q=Minions
        private static string BaseURL = "http://www.imdb.com/xml/find?json=1&tt=on&q=";

        public IList<Movie> getAllMovies(string name)
        {
            //ToDo implement
            var json = new WebClient().DownloadString(BaseURL + name);
            /*
            var result = JsonConvert.DeserializeObject<IEnumerable<Movie>>(json);
            foreach(var res in result)
            {
                Console.WriteLine(res.Title.Name);
            }
            */
            return null;
        }
    }
}
