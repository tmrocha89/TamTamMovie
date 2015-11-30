using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TamTamMovie.Models.ModelView;
using TTMovieModel.DAL;
using TTMovieModel.Model;

namespace TamTamMovie.Models
{
    public class Repository
    {
        private DataAccess dataAccess;

        public object JSonConverter { get; private set; }

        public Repository()
        {
            dataAccess = new DataAccess();
        }

        private IList<Movie> getCachedMovies()
        {
            return MovieCache.GetMovies(null);
        }

        public async Task<IList<Movie>> GetMovies(string movieName)
        {
            Clear();
            IList<Movie> movies = null;
            movies = MovieCache.GetMovies(movieName);
            if(movies == null || movies.Count == 0)
            {
                movies = await dataAccess.GetMovies(movieName);
                MovieCache.AddMovies(movieName, movies);
            }

            return movies;
        }
/*
        public void LoadVideos()
        {
            IList<Movie> movies = MovieCache.GetMovies();
            foreach(Movie mov in movies)
            {
                IList<Video> videos = dataAccess.GetVideoFor(mov.ID);
            }
        }
        */
        public async Task<Image> GetCoverFor(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            if (movie != null && movie.hasCover())
            {
                return movie.Cover;
            }
            Image image = await dataAccess.GetCoverFor(movieID);
            if(image != null)
            {
                movie.Cover = image;
                MovieCache.UpdateMovie(movie);
            }
            return image;
        }

        public void Clear()
        {
            MovieCache.Clear();
        }

        public async Task<Movie> LoadDetailedData(string movieID)
        {
            Movie movie = MovieCache.getMovie(movieID);
            if (movie != null)
            {
                movie = await dataAccess.GetInformationFor(movie);
                movie.Trailers = dataAccess.GetVideoFor(movie.Title.Name);
                MovieCache.UpdateMovie(movie);
                return movie;
            }
            return null;
        }


        private IList<MovieDTO> MoviesToDTO(IList<Movie> movies)
        {
            IList<MovieDTO> dtoMovies = new List<MovieDTO>();
            foreach (var movie in movies)
                dtoMovies.Add(MovieToDTO(movie));
            return dtoMovies;
        }

        private MovieDTO MovieToDTO(Movie movie)
        {
            MovieDTO dto = new MovieDTO();
            dto.ID = movie.ID;
            dto.Year = movie.Year;
            dto.Title = movie.Title.Name;
            foreach (var star in movie.Stars)
            {
                dto.Stars.Add(star.Name);
            }
            foreach (var writer in movie.Writers)
            {
                dto.Writers.Add(writer.Name);
            }
            foreach (var director in movie.Directors)
            {
                dto.Directors.Add(director.Name);
            }
            foreach (var genre in movie.Genres)
            {
                dto.Genres.Add(genre.Name);
            }
            foreach (var trailer in movie.Trailers)
            {
                dto.Trailers.Add(new VideoDTO(trailer.Link, trailer.Thumbnail.Link));
            }
            dto.Resume = movie.Resume;
            dto.CoverUrl = movie.Cover != null ? movie.Cover.Link : null;
            
            return dto;
        }


        public async Task<string> GetMoviesToSend(string movieName, bool details)
        {
            IList<Movie> movies = await GetMovies(movieName);
            if (details) {
                for(int i=0; i < movies.Count; i++)
                {
                    movies[i] = await LoadDetailedData(movies[i].ID);
                }
            }

            var json = JsonConvert.SerializeObject( MoviesToDTO(movies) );
            return json;
        }

        public async  Task<string> GetCovers(string movieName)
        {
            IList<Movie> movies = getCachedMovies();
            IList<string> images = new List<string>();
            foreach(var movie in movies)
            {
                Image tmpImage = await GetCoverFor(movie.ID);
                images.Add(tmpImage.Link);

            }
            var json = JsonConvert.SerializeObject(images);
            return json;
        }

        public async Task<string> GetMoviesToSend(string movieID, bool details, bool cover)
        {
            MovieDTO dtoMovie;
            if (details)
            {
                dtoMovie = MovieToDTO(await LoadDetailedData(movieID));
            }else if (cover)
            {
                Movie movie = MovieCache.getMovie(movieID);
                movie.Cover = await GetCoverFor(movieID);
                dtoMovie = MovieToDTO(movie);
            }
            else
            {
                return "{error: Invalid Request}";
            }

            var json = JsonConvert.SerializeObject(dtoMovie);
            return json;
        }
    }
}