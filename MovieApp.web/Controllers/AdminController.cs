using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.web.Data;
using MovieApp.web.Entity;
using MovieApp.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly MovieContext _context;
        public AdminController(MovieContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult MovieUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _context.Movies.Select(m => new AdminEditMovieViewModel
            {
                MovieId = m.MovieId,
                Title = m.Title,
                Description = m.Description,
                ImageUrl = m.ImageUrl,
                SelectedGenres = m.Genres,
                TrailerUrl=m.TrailerUrl
            }).FirstOrDefault(m => m.MovieId == id);

            ViewBag.Genres = _context.Genres.ToList();

            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> MovieUpdate(AdminEditMovieViewModel model, int[] genreIds, IFormFile file)
        {
            var entity = _context.Movies.Include(m => m.Genres).FirstOrDefault(m => m.MovieId == model.MovieId);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.TrailerUrl = model.TrailerUrl;


            if (file != null && file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                entity.ImageUrl = fileName;
            }

            entity.Genres.Clear();
            entity.Genres = genreIds.Select(id => _context.Genres.FirstOrDefault(i => i.GenreId == id)).ToList();

            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }

        public IActionResult MovieList()
        {
            var movies = _context.Movies
                .Include(m => m.Genres)
                .Select(m => new AdminMovieViewModel
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    ImageUrl = m.ImageUrl,
                    Genres = m.Genres.ToList()
                })
                .ToList();

            var viewModel = new AdminMoviesViewModel
            {
                Movies = movies
            };

            return View(viewModel);
        }

        public IActionResult GenreList()
        {
            var model = new AdminGenresViewModel
            {
                Genres = _context.Genres.Select(g => new AdminGenreViewModel
                {
                    GenreId = g.GenreId,
                    Name = g.Name,
                    Count = g.Movies.Count()
                }).ToList()
            };

            return View(model);
        }

        public IActionResult GenreUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _context
                .Genres
                .Select(g => new AdminGenreEditViewModel
                {
                    GenreId = g.GenreId,
                    Name = g.Name,
                    Movies = g.Movies.Select(i => new AdminMovieViewModel
                    {
                        MovieId = i.MovieId,
                        Title = i.Title,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                })
                .FirstOrDefault(g => g.GenreId == id);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult GenreUpdate(AdminGenreEditViewModel model, int[] movieIds)
        {
            var entity = _context.Genres.Include("Movies").FirstOrDefault(i => i.GenreId == model.GenreId);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            foreach (var id in movieIds)
            {
                var movie = entity.Movies.FirstOrDefault(m => m.MovieId == id);
                if (movie != null)
                {
                    entity.Movies.Remove(movie);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("GenreList");
        }

        [HttpPost]
        public IActionResult GenreDelete(int genreId)
        {
            var entity = _context.Genres.Find(genreId);
            if (entity != null)
            {
                _context.Genres.Remove(entity);
                _context.SaveChanges();
            }

            return RedirectToAction("GenreList");
        }

        [HttpPost]
        public IActionResult MovieDelete(int MovieId)
        {
            var entity = _context.Movies.Find(MovieId);
            if (entity != null)
            {
                _context.Movies.Remove(entity);
                _context.SaveChanges();
            }

            return RedirectToAction("MovieList");
        }

        public IActionResult MovieCreate()
        {
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MovieCreate(AdminEditMovieViewModel model, int[] genreIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Title = model.Title,
                    Description = model.Description,
                    TrailerUrl=model.TrailerUrl

                };

                if (file != null && file.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    movie.ImageUrl = fileName;
                }

                if (genreIds != null && genreIds.Length > 0)
                {
                    movie.Genres = genreIds.Select(id => _context.Genres.Find(id)).ToList();
                }

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();

                return RedirectToAction("MovieList");
            }

            ViewBag.Genres = _context.Genres.ToList();
            return View(model);
        }

    }
}
