using MovieApp.web.Entity;
using System.Collections.Generic;

namespace MovieApp.web.Models
{
    public class AdminMoviesViewModel
    {
        public List<AdminMovieViewModel> Movies { get; set; }
    }
    public class AdminMovieViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public List<Genre> Genres { get;  set; }

    }
    public class AdminEditMovieViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Director { get; set; }
        public List<Genre> SelectedGenres { get; set; }
        public string TrailerUrl { get; set; }

    }
}
