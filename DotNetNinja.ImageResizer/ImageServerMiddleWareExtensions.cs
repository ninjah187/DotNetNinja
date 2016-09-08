using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace DotNetNinja.AspNetCore.ImageResizer
{
    public static class ImageServerMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageServer(this IApplicationBuilder app)
            => app.UseMiddleware<ImageServerMiddleware>();
    }
}
