using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TamTamMovie.Models.ModelView
{
    public class MovieDTO
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public IList<string> Writers { get; set; }
        public IList<string> Directors { get; set; }
        public IList<string> Stars { get; set; }
        public IList<string> Genres { get; set; }
        public string Resume { get; set; }
        public string CoverUrl { get; set; }
        public IList<VideoDTO> Trailers { get; set; }

        public MovieDTO()
        {
            Writers = new List<string>();
            Directors = new List<string>();
            Stars = new List<string>();
            Genres = new List<string>();
            Trailers = new List<VideoDTO>();
        }
    }
}