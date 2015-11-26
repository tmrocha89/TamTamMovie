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

        private static string MOVIE_ID = "id";
        private static string MOVIE_TITLE = "title";
        private static string MOVIE_NAME = "name";
        private static string MOVIE_TITLE_DESCRIPTION = "title_description";
        private static string MOVIE_EPISODE_TITLE = "episode_title";
        private static string MOVIE_DESCRIPTION = "description";
        

        public static Movie GetBasicMovieInformation(JToken jsonMovie)
        {
            Movie movie = new Movie();
            movie.ID = GetID(jsonMovie);
            movie.Title = GetTitle(jsonMovie);
            movie.Year = GetYear(jsonMovie);
           // movie.Writers = GetWritersList(jsonMovie);
            //movie.Directors = GetDirectorsList(jsonMovie);
            //movie.Stars = GetStarsList(jsonMovie);
            //movie.Genres = GetGenresList(jsonMovie);
            movie.Resume = GetResumo(jsonMovie);

            return movie;
        }

        private static string GetResumo(JToken jsonMovie)
        {
            return "";
        }

        private static IList<Genre> GetGenresList(JToken jsonMovie)
        {
            throw new NotImplementedException();
        }

        private static IList<Star> GetStarsList(JToken jsonMovie)
        {
            throw new NotImplementedException();
        }

        private static IList<Director> GetDirectorsList(JToken jsonMovie)
        {
            throw new NotImplementedException();
        }

        private static IList<Writer> GetWritersList(JToken jsonMovie)
        {
            throw new NotImplementedException();
        }

        private static string GetID(JToken jsonMovie)
        {
            return (string)jsonMovie[MOVIE_ID];
        }

        private static Title GetTitle(JToken jsonMovie)
        {
            Title title = new Title( (string)jsonMovie[MOVIE_TITLE] );
            return title;
        }

        private static int GetYear(JToken jsonMovie)
        {
            string strYear = Regex.Match( (string)jsonMovie[MOVIE_DESCRIPTION],  @"^\d{4}").Value;
            int year = -1;
            int.TryParse(strYear, out year);

            return year;            
        }
    }
}
