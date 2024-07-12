using MovieApp.web.Entity;
using System.Collections.Generic;

namespace MovieApp.web.Models
{
    public class HomePageViewModel
    {
        public List<Movie> PopularMovies
        {
            get; set;
        }
    }
}
