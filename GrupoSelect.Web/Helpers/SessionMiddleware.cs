using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http;
using System.Security.Claims;

namespace GrupoSelect.Web.Helpers
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISessionProvider sessionProvider)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                int userId = 0;
                string userName = string.Empty;
                string profile = string.Empty;

                IEnumerable<Claim> claimsIdentity = context.User.Claims;

                var userIdClaim = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value != null ? int.Parse(userIdClaim.Value) : 0;
                }

                var nameClaim = claimsIdentity.SingleOrDefault(c => c.Type == ClaimTypes.Name);

                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }

                var profileClaim = claimsIdentity.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                if (profileClaim != null)
                {
                    profile = profileClaim.Value;
                }

                sessionProvider.Set(userId, userName, profile);
            }

            await _next(context);
        }
    }
}