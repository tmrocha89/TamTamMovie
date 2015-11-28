using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class VimeoVideoProvider : IVideoProvider
    {
        public IList<Video> GetVideoFor(string movieID)
        {
            throw new NotImplementedException();
        }
    }
}
