using System.Collections.Generic;

namespace TTMovieModel.Model
{
    public class Movie
    {
        public string ID { get; set; }
        public int Year { get; set; }
        public IList<Writer> Writers { get; }
        public IList<Director> Directors { get; }
        public IList<Star> Stars { get; }
        public IList<Genre> Genres { get; }
        public string Resume { get; set; }

        public Movie()
        {
            Writers = new List<Writer>();
            Directors = new List<Director>();
            Stars = new List<Star>();
            Genres = new List<Genre>();
        }

        public void AddWriter(Writer writer)
        {
            if(writer != null)
            {
                Writers.Add(writer);
            }
        }

        public void AddDirector(Director director)
        {
            if(director != null)
            {
                Directors.Add(director);
            }
        }

        public void AddStar(Star star)
        {
            if(star != null)
            {
                Stars.Add(star);
            }
        }

        public void AddGenre(Genre genre)
        {
            if(genre != null)
            {
                Genres.Add(genre);
            }
        }

    }
}
