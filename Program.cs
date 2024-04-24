using eLibrary.Data;
using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;
using eLibrary.Infrastructure;
using eLibrary.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace eLibrary
{
    public class Program
    {
        public static void Main (string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ITracksService, TracksService>();
            builder.Services.AddScoped<IRepository<User>, EFRepository<User>>();
            builder.Services.AddScoped<IRepository<Role>, EFRepository<Role>>();

            builder.Services.AddScoped<IRepository<Track>, EFRepository<Track>>();
            builder.Services.AddScoped<IRepository<Playlist>, EFRepository<Playlist>>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITracksReader, TracksReader>();
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.ExpireTimeSpan = TimeSpan.FromHours(1);
                    opt.Cookie.Name = "library_session";
                    opt.Cookie.HttpOnly = true;
                    opt.Cookie.SameSite = SameSiteMode.Strict;
                    opt.LoginPath = "/User/Login";
                    opt.AccessDeniedPath = "/User/AccessDenied";
                });
            builder.Services.AddDbContext<ELibraryContext>(opt => opt.UseSqlServer("Server=.; Database=DaatabaseHome15; Integrated Security = true; TrustServerCertificate=Yes"));
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer("Server=.; Database=DaatabaseHome15; Integrated Security = true; TrustServerCertificate=Yes");
            using (var context = new ELibraryContext(optionsBuilder.Options))//вызов посева начальных данных
            {
                EFInitialSeed.Seed(context);
            }
            var app = builder.Build();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseTracksProtection();
            app.UseStaticFiles();
            app.MapControllerRoute("default", "{Controller=Tracks}/{Action=Index}");
            app.MapControllerRoute("default", "{Controller=Playlists}/{Action=Index}");
            app.Run();
        }
    }
}
