using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.web.Data;
using MovieApp.web.Entity;
using MovieApp.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int? id, string q)
        {
            var movies = _context.Movies.AsQueryable();
            if (id != null)
            {
                movies = movies.Include(m => m.Genres).Where(m => m.Genres.Any(g => g.GenreId == id));
            }

            if (!string.IsNullOrEmpty(q))
            {
                movies = movies.Where(i =>
                i.Title.ToLower().Contains(q.ToLower()) ||
                i.Description.ToLower().Contains(q.ToLower()));
            }
            var model = new MoviesViewModel()
            {
                Movies = movies.ToList()
            };
            return View("Movies", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genres) // İlgili genre bilgilerini de alabiliriz
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsWatched(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                var user = _context.Users.Where(a => a.Email == email).FirstOrDefault();
                if (user.Role == null)
                {
                    return BadRequest("Invalid user ID.");
                }
                else
                {
                    var watchedMovie = new WatchedMovie
                    {
                        MovieId = movie.MovieId,
                        UserId = user.userId,
                        WatchedDate = DateTime.Now
                    };
                    _context.WatchedMovies.Add(watchedMovie);
                }



                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest();
            }



        }

        [HttpPost]
        public async Task<IActionResult> MarkAsWatchLater(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
                if (user.Role==null)
                {
                    return BadRequest("Invalid user ID.");
                }
                else
                {
                    var watchLaterMovie = new WatchLaterMovie
                    {
                        MovieId = movie.MovieId,
                        UserId = user.userId, 
                        AddedDate = DateTime.Now
                    };
                    await _context.WatchLaterMovies.AddAsync(watchLaterMovie);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
