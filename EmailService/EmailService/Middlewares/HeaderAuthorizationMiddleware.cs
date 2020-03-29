using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EmailService.Middlewares
{
    public class HeaderAuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string key;

        public HeaderAuthorizationMiddleware(RequestDelegate next, string key)
        {
            this.next = next;
            this.key = key;
        }

        public async Task Invoke(HttpContext context)
        {

            string authHeader = context.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                if (authHeader.Equals(key))
                {
                    await next.Invoke(context);
                    return;
                }
            }

            //Reject request if there is no authorization header or if it is not valid
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
        }
    }

    public static class HeaderAuthorizationMiddlewareExtension
    {
        public static IApplicationBuilder UseHeaderAuthorization(this IApplicationBuilder app, string key)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<HeaderAuthorizationMiddleware>(key);
        }
    }
}
