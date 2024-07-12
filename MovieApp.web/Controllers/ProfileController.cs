using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.web.Data;
using MovieApp.web.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly MovieContext _context;

        public ProfileController(MovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexProfile()
        {
            // Kullanıcı kimliğini al
            var username = User.Identity.Name;

            // Kullanıcıyı veritabanından al
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcının izlediği filmleri al
            var watchedMovies = await _context.WatchedMovies
                .Where(wm => wm.UserId == user.userId)
                .Include(wm => wm.Movie)
                .Select(wm => wm.Movie)
                .ToListAsync();

            // Kullanıcının daha sonra izleyeceği filmleri al
            var watchLaterMovies = await _context.WatchLaterMovies
                .Where(wlm => wlm.UserId == user.userId)
                .Include(wlm => wlm.Movie)
                .Select(wlm => wlm.Movie)
                .ToListAsync();

            // Profil görünüm modeli oluştur
            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                WatchedMovies = new MoviesViewModel { Movies = watchedMovies },
                WatchLaterMovies = new MoviesViewModel { Movies = watchLaterMovies }
            };

            return View(model);
        }
    }
}
