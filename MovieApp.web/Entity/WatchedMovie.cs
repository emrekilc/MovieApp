using System;

namespace MovieApp.web.Entity
{
    public class WatchedMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime WatchedDate { get; set; }

        public Movie Movie { get; set; }
    }
}