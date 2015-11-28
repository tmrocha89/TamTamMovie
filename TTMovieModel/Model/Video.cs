using System;

namespace TTMovieModel.Model
{
    public class Video
    {
        public string Link { get; private set; }
        public Image Thumbnail { get; private set; }

        public Video(string link, Image image)
        {
            SetLink(link);
            SetImage(image);
        }

        private void SetLink(string link)
        {
            if (!String.IsNullOrWhiteSpace(link))
            {
                this.Link = link;
            }
        }

        private void SetImage(Image image)
        {
            if(image != null)
            {
                Thumbnail = image;
            }
        }
    }
}
