using eLibrary.Domain.Entities;
using eLibrary.Domain.Services;
using eLibrary.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace eLibrary.Controllers
{
    public class UserController :Controller
    {
        private const int adminRoleId = 2;
        private const int clientRoleId = 1;

        public IActionResult Login ()
        {
            return View();

        }

        [HttpGet]
        public IActionResult Registration ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration (RegistrationViewModel registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            if (await userService.IsUserExistsAsync(registration.Username))
            {
                ModelState.AddModelError("user_exists", $"Имя пользователя {registration.Username} уже существует!");
                return View(registration);
            }

            try {
                await userService.RegistrationAsync(registration.Fullname, registration.Username, registration.Password);
                return RedirectToAction("RegistrationSuccess", "User");
            } catch
            {
                ModelState.AddModelError("reg_error", $"Не удалось зарегистрироваться, попробуйте попытку регистрации позже");
                return View(registration);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel login) {
            if (!ModelState.IsValid) {
                return View(login);
            }
            User? newUser = await userService.GetUserAsync(login.Username, login.Password);
            if (newUser == null) {
                ModelState.AddModelError("reg_error", $"неверное имя пользователя или пароль");
                return View(login);
            }
            await SignIn(newUser);
            return RedirectToAction("Index", "Books");
            
        }

        private async Task SignIn (User user) {
            string role = user.RoleId switch
            {
                adminRoleId => "admin",
                clientRoleId => "client",
                _ => throw new ApplicationException("invalid user role")
            };

            List<Claim> claims = new List<Claim>
            {
                new Claim("fullname", user.Fullname),
                new Claim("id", user.Id.ToString()),
                new Claim("role", role),
                new Claim("username", user.Login)
            };
            string authType = CookieAuthenticationDefaults.AuthenticationScheme;
            IIdentity identity = new ClaimsIdentity(claims, authType, "username", "role");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }

        public IActionResult RegistrationSuccess () {
            return View();
        }

        private readonly IUserService userService;

        public UserController (IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult AccessDenied ()
        {
            return View();
        }
    }
}
