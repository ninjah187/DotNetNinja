using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DotNetNinja.AspNetCore.UserAccess
{
    public class UnauthorizedRedirectionMiddleware
    {
        readonly RequestDelegate _next;
        UnauthorizedRedirectionOptions _options;

        public UnauthorizedRedirectionMiddleware(RequestDelegate next, UnauthorizedRedirectionOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401)
            {
                context.Response.Redirect(_options.RouteValue);
            }
        }
    }
}
