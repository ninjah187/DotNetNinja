using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace DotNetNinja.AspNetCore.UserAccess
{
    public static class UnauthorizedRedirectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnauthorizedRedirection(this IApplicationBuilder builder, string routeValue)
            => builder.UseMiddleware<UnauthorizedRedirectionMiddleware>(new UnauthorizedRedirectionOptions(routeValue));
    }
}
