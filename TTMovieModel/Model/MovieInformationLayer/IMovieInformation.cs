﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMovieModel.Model
{
    public interface IMovieInformation
    {
        Task<IList<Movie>> getAllMovies(string name);
        Task<Image> GetCoverFor(string movieID);
        Task<Movie> GetInformationFor(Movie movie);
    }
}
