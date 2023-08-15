using GrupoSelect.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GrupoSelect.Web.Helpers
{
    public class SecurityAttribute : IAuthorizationFilter
    {
        private readonly IUserService _userService;

        public SecurityAttribute(IUserService userService)
        {
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Access", action = "Login" }));
            }
            else
            {
                int userId = Convert.ToInt32(context.HttpContext.User.GetId());

                var result = _userService.GetById(userId).Result;

                if (result.Success)
                {
                    if (!result.Object.Active)
                    {
                        context.HttpContext.SignOutAsync();
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Access", action = "Login" }));
                    }
                }
                else
                {
                    context.HttpContext.SignOutAsync();
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Access", action = "Login" }));
                }
            }
        }
    }
}