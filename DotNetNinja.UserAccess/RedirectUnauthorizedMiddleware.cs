using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DotNetNinja.UserAccess
{
    public class RedirectUnauthorizedMiddleware
    {
        readonly RequestDelegate _next;
        RedirectUnauthorizedOptions _options;

        public RedirectUnauthorizedMiddleware(RequestDelegate next, RedirectUnauthorizedOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401)
            {
                context.Response
            }
        }
    }
}
