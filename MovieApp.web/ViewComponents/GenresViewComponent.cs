using Microsoft.AspNetCore.Mvc;
using MovieApp.web.Data;
using MovieApp.web.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.web.ViewComponents

{
    public class GenresViewComponent : ViewComponent
    {
        private readonly MovieContext _context;
        public GenresViewComponent(MovieContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {

            ViewBag.SelectedGenre = RouteData.Values["id"];
            return View(_context.Genres.ToList());
        }
    }
}
