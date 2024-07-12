
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.web.Entity
{
    public class Genre
    {
        public string Name { get; set; }
        //[Required]
        public int GenreId { get; set; }
        public List<Movie> Movies{ get; set; }
    }
}
