using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace kursovoi_4kurs.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TracksProtectionMiddleware
    {
        private readonly RequestDelegate _next;

        public TracksProtectionMiddleware (RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke (HttpContext httpContext)//проверим что пользователь аутентифицирован
        {

            if (httpContext.Request.Path.ToString().StartsWith("/tracks/"))
            {
                if (httpContext.User.Identity?.IsAuthenticated != true)
                {
                    httpContext.Response.Redirect("/user/login/");
                    return Task.CompletedTask;
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TracksProtectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseTracksProtection (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TracksProtectionMiddleware>();
        }
    }
}
