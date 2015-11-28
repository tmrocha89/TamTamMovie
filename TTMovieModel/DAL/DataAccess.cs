using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMovieModel.Model;

namespace TTMovieModel.DAL
{
    public class DataAccess
    {

        private MovieDataAccess movieDA;
        private SocialNetworkDataAccess socialDA;

        public DataAccess()
        {
            movieDA = new MovieDataAccess();
            socialDA = new SocialNetworkDataAccess();
        }

        public async Task<IList<Movie>> GetMovies(string movieName)
        {
            return await movieDA.GetMovies(movieName);
        }

        public IList<Video> GetVideoFor(string videoName)
        {
            return movieDA.GetVideoFor(videoName);
        }

        public async Task<Image> GetCoverFor(string movieID)
        {
            return await movieDA.GetCoverFor(movieID);
        }
    }
}
