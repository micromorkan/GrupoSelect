using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using GrupoSelect.Web.Models;
using GrupoSelect.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace GrupoSelect.Web.Controllers
{
    public class AccessController : Controller
    {
        private IAccessService _accessService;

        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var result = await _accessService.Authenticate(new Domain.Entity.User { Login = login.User, Password = login.Password });

            if (result.Success)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Object.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.Object.Name),
                    new Claim(ClaimTypes.Role, result.Object.Profile),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = login.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["GS_ERRORS_ACCESS"] = result.Errors;
                ViewData["GS_AUTH_ERROR"] = result.Message;

                return View(login);
            }
        }
    }
}
