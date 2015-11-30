using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class Image
    {
        public string Link { get; private set; }

        public Image(string link)
        {
            SetLink(link);
        }

        private void SetLink(string link)
        {
            if (!String.IsNullOrWhiteSpace(link) && link != "N/A")
            {
                this.Link = link;
            }
            else
            {
                Link = null;
            }
        }
    }
}
