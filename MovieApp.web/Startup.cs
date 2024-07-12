using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApp.web.Data;
using MovieApp.web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MsSQLConnection")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/";
            });
            services.AddControllersWithViews(config =>
            {
                // Global yetkilendirme filtresi ekleyin
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddControllersWithViews(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //DataSeedings.Seed(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();//wwwroot klasörünü kullanýma açar

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession(); // Add session middleware here

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}"
                    );

                //localhost:54556/{controller}/{action}
                //         endpoints.MapControllerRoute(
                //            name: "home",
                //            pattern: "",
                //             defaults: new { controller = "Home", action = "Index" }
                //             );
                //         endpoints.MapControllerRoute(
                //             name: "about",
                //             pattern: "hakkimizda",
                //             defaults: new { controller = "Home", action = "About" }
                //);
                //         endpoints.MapControllerRoute(
                //             name: "movielist",
                //             pattern:"movies/list",
                //             defaults:new {controller ="Movies",action="List"}
                //             );
                //         endpoints.MapControllerRoute(
                //             name: "movieDetails",
                //             pattern:"movies/details",
                //             defaults:new {controller ="Movies",action="Details"}
                //             );
            });
        }
    }
}
