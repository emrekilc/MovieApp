
using Microsoft.EntityFrameworkCore;
using MovieApp.web.Entity;

namespace MovieApp.web.Data

{
    public class MovieContext: DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

            
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set; }
        public DbSet<WatchLaterMovie> WatchLaterMovies { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=Movies.db");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Movie>().Property(b => b.Title).HasMaxLength(500);

            modelBuilder.Entity<Genre>().Property(b => b.Name).IsRequired();
            modelBuilder.Entity<Genre>().Property(b => b.Name).HasMaxLength(50);
        }


    }

}
