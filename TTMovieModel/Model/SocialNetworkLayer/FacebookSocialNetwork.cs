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
    public class FacebookSocialNetwork : ISocialNetwork
    {
        private static string AppID = "1644308279183059";
        private static string AppSecret = "f4f0d8646292bebd4e7bdeb474ea04a7";

        public string Name { get; set; }


        public FacebookSocialNetwork()
        {
            //init();
        }

        public async void init()
        {
            string url_autgh = "https://www.facebook.com/dialog/oauth?client_id ={" + AppID + "}&redirect_uri ={http://localhost:51191/Home/Index}";
            using (WebClient webClient = new WebClient())
            {
                var jsonText = await webClient.DownloadStringTaskAsync(url_autgh);

                JObject movieJson = JObject.Parse(jsonText);

                Debug.WriteLine(movieJson);
            }
        }

    }
}
