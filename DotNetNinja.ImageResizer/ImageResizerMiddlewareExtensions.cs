using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace DotNetNinja.ImageResizer
{
    public static class ImageResizerMiddlewareExtensions
    {
        //public static IApplicationBuilder UseImageResizer(this IApplicationBuilder app, IImageResizer imageResizer, IImagePathGenerator pathGenerator)
        //    => app.UseMiddleware<ImageResizerMiddleware>(imageResizer, pathGenerator);

        public static IApplicationBuilder UseImageResizer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ImageResizerMiddleware>(new ImageResizerBuilder()
                                                                .AllowAll()
                                                                .Build());
        }

        public static IApplicationBuilder UseImageResizer(this IApplicationBuilder app, Func<IImageResizerBuilder, IImageResizerBuilder> buildResizer)
        {
            var builder = new ImageResizerBuilder();

            var resizer = buildResizer(builder).Build();

            return app.UseMiddleware<ImageResizerMiddleware>(resizer);
        }
    }
}
