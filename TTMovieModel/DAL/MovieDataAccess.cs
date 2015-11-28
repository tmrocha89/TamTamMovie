using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTMovieModel.Model;

namespace TTMovieModel.DAL
{
    public class MovieDataAccess
    {
        //private IDictionary<string, IMovieInformation> movieInfo;
        private IMovieInformation movieInfo;
        private IVideoProvider videoProvider;
        public MovieDataAccess()
        {
            movieInfo = new ImdbMovieInformation();
            videoProvider = new YoutubeVideoProvider();
        }

        public async Task<IList<Movie>> GetMovies(string movieName)
        {
            return await movieInfo.getAllMovies(movieName);
        }

        public IList<Video> GetVideoFor(string videoName)
        {
            return videoProvider.GetVideoFor(videoName);
        }

        public async Task<Image> GetCoverFor(string movieID)
        {
            return await movieInfo.GetCoverFor(movieID);
        }

        public Task<Movie> GetInformationFor(Movie movie)
        {
            return movieInfo.GetInformationFor(movie);
        }
    }
}
