using System;
using System.Collections.Generic;
using MovieApp.web.Entity;

namespace MovieApp.web.Models
{
    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MoviesViewModel WatchedMovies { get; set; }
        public MoviesViewModel WatchLaterMovies { get; set; }
    }
}
