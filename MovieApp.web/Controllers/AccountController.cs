using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.web.Data;
using MovieApp.web.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MovieApp.web.Entity;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MovieApp.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly MovieContext _context;

        public AccountController(MovieContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = ComputeSha256Hash(model.Password);
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username && u.Password == hashedPassword);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role) 
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Session değerini ayarla
                    HttpContext.Session.SetString("uyevarmi", "1");

                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Role == "User")
                    {
                        return RedirectToAction("Index", "Home");
                    } // Başarılı girişten sonra Home controller'ına yönlendirme
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
[AllowAnonymous]
public async Task<IActionResult> Register(RegisterViewModel model)
{
    if (ModelState.IsValid)
    {
        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match.");
            return View(model);
        }

        var hashedPassword = ComputeSha256Hash(model.Password);
        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            Password = hashedPassword, // Store the hashed password
            FullName = model.FullName,
            DateOfBirth = model.DateOfBirth,
            Role="User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role) // Kullanıcının rolünü ekleyin
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        // Session değerini ayarla
        HttpContext.Session.SetString("uyevarmi", "1");

        return RedirectToAction("Index", "Home");
    }

    return View(model);
}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("uyevarmi");
            return RedirectToAction("Login");
        }
     

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
