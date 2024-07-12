using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.web.Entity
{
    public class Movie
    {

        public int MovieId { get; set; }
        //[Required]
        public string Title { get; set; }
        //[MaxLength(10000)]
        public string Description { get; set; }

        // public string[] Players { get; set; }
        public string ImageUrl { get; set; }
        //[Required]
        public List<Genre> Genres { get; set; }
        public string TrailerUrl { get; set; } // YouTube fragman URL'si için yeni özellik
    }

}