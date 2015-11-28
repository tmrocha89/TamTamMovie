using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMovieModel.Model;

namespace TTMovieModel.DAL
{
    public class SocialNetworkDataAccess
    {

        private IDictionary<string, ISocialNetwork> socialNetworks;

        public SocialNetworkDataAccess()
        {
            socialNetworks = new Dictionary<string, ISocialNetwork>();
            //this data should be read from any config file
            socialNetworks["Facebook"] = new FacebookSocialNetwork();
        }

        public IList<string> GetSocialNetworkAvailable()
        {
            IList<string> networksAvailable = new List<string>();
            foreach(var network in socialNetworks)
            {
                networksAvailable.Add(network.Value.Name);
            }
            return networksAvailable;
        }

    }
}
