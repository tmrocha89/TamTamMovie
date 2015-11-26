using System;

namespace TTMovieModel.Model
{
    public class Video
    {
        public string Link { get; private set; }

        public Video(string link)
        {
            SetLink(link);
        }

        private void SetLink(string link)
        {
            if (!String.IsNullOrWhiteSpace(link))
            {
                this.Link = link;
            }
        }
    }
}
