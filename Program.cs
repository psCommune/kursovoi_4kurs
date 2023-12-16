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
            builder.Services.AddScoped<IBooksService, BooksService>();
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
            builder.Services.AddDbContext<ELibraryContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("local")));
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("local"));
            /*using (var context = new ELibraryContext(optionsBuilder.Options))//вызов посева начальных данных
            {
                EFInitialSeed.Seed(context);
            }*/
            builder.Services.AddScoped<IRepository<User>, EFRepository<User>>();
            builder.Services.AddScoped<IRepository<Role>, EFRepository<Role>>();
            builder.Services.AddScoped<IRepository<Book>, EFRepository<Book>>();
            builder.Services.AddScoped<IRepository<Category>, EFRepository<Category>>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBooksReader, BooksReader>();
            var app = builder.Build();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBooksProtection();
            app.UseStaticFiles();
            app.MapControllerRoute("default", "{Controller=Books}/{Action=Index}");

            app.Run();
        }
    }
}
