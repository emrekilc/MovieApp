using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.web.Data;
using MovieApp.web.Entity;
using MovieApp.web.Models;
using SQLitePCL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MovieApp.web.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly MovieContext _context;
        public HomeController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                PopularMovies = _context.Movies.ToList()
            };



            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public IActionResult About()
        {
            var turListesi = new List<Genre>()
            {
                new Genre{Name="Macera"},
                new Genre{Name="Romantik"},
                new Genre{Name="Komedi"},
                new Genre{Name="Savaş"}
            };
            return View(turListesi);
        }

        public ActionResult contact()
        {
            return View();
        }
    }
}
