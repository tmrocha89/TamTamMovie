using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public class ImdbParser
    {
        // imdb
        private const string MOVIE_ID = "id";
        private const string MOVIE_TITLE = "title";
        private const string MOVIE_DESCRIPTION = "description";
        // omdb
        private const string MOVIE_IMAGE = "Poster";
        private const string MOVIE_STARS = "Actors";
        private const string MOVIE_WRITER = "Writer";
        private const string MOVIE_DIRECTORES = "Director";
        private const string MOVIE_GENRES = "Genre";
        private const string MOVIE_RESUME = "Plot";

        public Movie GetBasicMovieInformation(JToken jsonMovie)
        {
            Movie movie = new Movie();
            movie.ID = GetID(jsonMovie);
            movie.Title = GetTitle(jsonMovie);
            movie.Year = GetYear(jsonMovie);
            return movie;
        }

        private HtmlNode getHtmlNode(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//table[@id='title-overview-widget-layout']");

        }

        /*
        public Movie GetMovieWithImage(HtmlDocument htmlDoc, Movie movie)
        {
             movie.Image = getImage(getHtmlNode(htmlDoc));
            return movie;
        }
        */
/*
        public Movie GetDetailInformation(HtmlDocument htmlDoc, Movie movie)
        {
            var htmlNodes = getHtmlNode(htmlDoc);
            movie.Directors = getDirectors(htmlNodes);
            movie.Writers = getWriters(htmlNodes);
            movie.Stars = getStars(htmlNodes);
            movie.Genres = getGenres(htmlNodes);
            movie.Image = getImage(htmlNodes);
            return movie;
        }
*/
        private string GetID(JToken jsonMovie)
        {
            return (string)jsonMovie[MOVIE_ID];
        }

        private Title GetTitle(JToken jsonMovie)
        {
            Title title = new Title( (string)jsonMovie[MOVIE_TITLE] );
            return title;
        }

        private int GetYear(JToken jsonMovie)
        {
            string strYear = Regex.Match( (string)jsonMovie[MOVIE_DESCRIPTION],  @"^\d{4}").Value;
            int year = -1;
            int.TryParse(strYear, out year);

            return year;            
        }

        public Movie GetDetailInformation(JObject movieJson, Movie movie)
        {
            if ((string)movieJson["Response"] == "False")
                return movie;

            movie.Cover = getImage(movieJson);
            movie.Resume = getResume(movieJson);
            movie.Directors = getDirectors(movieJson);
            movie.Writers = getWriters(movieJson);
            movie.Genres = getGenres(movieJson);
            movie.Stars = getStars(movieJson);
            return movie;
        }

        public Image GetCover(JObject movieJson)
        {
            return getImage(movieJson);
        }

        private Image getImage(JToken jtoken)
        {
            Image image = new Image((string)jtoken[MOVIE_IMAGE]);
            return image;
        }

        private IList<Director> getDirectors(JToken jtoken)
        {
            IList<Director> directors = new List<Director>();
            String[] directorrArr = ((string)jtoken[MOVIE_WRITER]).Split(',');
            for (int i = 0; i < directorrArr.Length; i++)
                directors.Add(new Director(directorrArr[i]));
            return directors;
        }

        private IList<Writer> getWriters(JToken jtoken)
        {
            IList<Writer> writers = new List<Writer>();
            String[] writerArr = ((string)jtoken[MOVIE_WRITER]).Split(',');
            for (int i = 0; i < writerArr.Length; i++)
                writers.Add(new Writer(writerArr[i]));
            return writers;
        }

        private IList<Star> getStars(JToken jtoken)
        {
            IList<Star> stars = new List<Star>();
            String[] starArr = ((string)jtoken[MOVIE_STARS]).Split(',');
            for (int i = 0; i < starArr.Length; i++)
                stars.Add(new Star(starArr[i]));
            return stars;
        }

        private IList<Genre> getGenres(JToken jtoken)
        {
            IList<Genre> genres = new List<Genre>();
            String[] genresArr = ((string)jtoken[MOVIE_GENRES]).Split(',');
            for (int i = 0; i < genresArr.Length; i++)
                genres.Add(new Genre(genresArr[i]) );
            return genres;
        }

        private string getResume(JToken jtoken)
        {
            return ((string)jtoken[MOVIE_RESUME]);
        }


        /*
        private Image getImage(HtmlNode node)
        {
            Image image = null;
            if (node == null)
                return image;
            try {
                image = new Image(node.SelectSingleNode("//*[@id='img_primary']/div[1]/a/img").GetAttributeValue("src", ""));
            }catch (NullReferenceException)
            {
                // there is no image :(
            }
            return image;
        }

        private IList<Director> getDirectors(HtmlNode node)
        {
            IList<Director> directors = new List<Director>();
            try {
                foreach (HtmlNode directorElem in node.SelectNodes("//*[@id='overview-top']/div[4]/a"))
                {
                    directors.Add(new Director(directorElem.InnerText));
                }
            }catch (NullReferenceException)
            {
                //there is no Directors
            }
            return directors;
        }

        private IList<Writer> getWriters(HtmlNode node)
        {
            IList<Writer> writers = new List<Writer>();
            try {
                foreach (HtmlNode writerElem in node.SelectNodes("//*[@id='overview-top']/div[5]/a"))
                {
                    writers.Add(new Writer(writerElem.InnerText));
                }
            }catch (NullReferenceException)
            {
                //there is no Writers
            }
            return writers;
        }

        private IList<Star> getStars(HtmlNode node)
        {
            IList<Star> stars = new List<Star>();
            try {
                foreach (HtmlNode starElem in node.SelectNodes("//*[@id='overview-top']/div[6]/a"))
                {
                    stars.Add(new Star(starElem.InnerText));
                }
            }
            catch (NullReferenceException)
            {
                //there is no Stars
            }
            return stars;
        }

        private IList<Genre> getGenres(HtmlNode node)
        {
            IList<Genre> genres = new List<Genre>();
            try
            {
                foreach (HtmlNode genreElem in node.SelectNodes("//*[@id='overview-top']/div[2]/a"))
                {
                    genres.Add(new Genre(genreElem.InnerText));
                }
            }catch (NullReferenceException)
            {
                //there is no Genres
            }
            return genres;
        }

    */
    }
}
