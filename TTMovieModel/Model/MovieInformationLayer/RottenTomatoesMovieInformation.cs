using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class RottenTomatoesMovieInformation : IMovieInformation
    {
        public Task<IList<Movie>> getAllMovies(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetCoverFor(string movieID)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetInformationFor(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
