using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamTamMovie.Models.ModelView
{
    public class VideoDTO
    {
        public string Link { get; set; }
        public string Thumbnail { get; set; }

        public VideoDTO(string link, string thumbnail)
        {
            this.Link = link;
            this.Thumbnail = thumbnail;
        }
    }
}