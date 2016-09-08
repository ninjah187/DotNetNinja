using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace DotNetNinja.AspNetCore.UserAccess
{
    //public static class UserAccessMiddlewareExtensions
    //{
    //    /// <summary>
    //    /// Adds UserAccess middleware to request pipeline.
    //    /// </summary>
    //    /// <param name="builder"></param>
    //    /// <returns></returns>
    //    public static IApplicationBuilder UseUserAccess(this IApplicationBuilder builder)
    //    {
    //        var options = new UserAccessOptions();

    //        return builder.UseUserAccess(options);
    //    }
        
    //    public static IApplicationBuilder UseUserAccess(this IApplicationBuilder builder, CookieAuthenticationOptions cookieOptions)
    //    {
    //        var options = new UserAccessOptions
    //        {
    //            CookieOptions = cookieOptions
    //        };

    //        return builder.UseUserAccess(options);
    //    }

    //    public static IApplicationBuilder UseUserAccess(this IApplicationBuilder builder, UserAccessOptions options)
    //    {
    //        return builder
    //            //.UseCookieAuthentication(options.CookieOptions)
    //            .UseMiddleware<UserAccessMiddleware>(options);
    //    }
    //}
}
