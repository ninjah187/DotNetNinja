using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DotNetNinja.ImageResizer
{
    public class ImageServerMiddleware
    {
        readonly RequestDelegate _next;
        readonly IImagePathGenerator _pathGenerator;
        readonly string[] _extensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif"
        };

        public ImageServerMiddleware(RequestDelegate next, IImagePathGenerator pathGenerator)
        {
            _next = next;
            _pathGenerator = pathGenerator;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var requestInputPath = request.Path.Value;

            if (IsImageRequested(requestInputPath))
            {
                var size = GetSize(request.Query);

                if (size.Equals(default(Size)))
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                //var inputPath = Path.GetFullPath(requestInputPath.Substring(1, requestInputPath.Length -1));
                var inputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", requestInputPath.Substring(1));

                if (!File.Exists(inputPath))
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                var requestOutputPath = _pathGenerator.GetResizedPath(requestInputPath, size);
                var outputPath = _pathGenerator.GetResizedPath(inputPath, size);

                if (File.Exists(outputPath))
                {
                    request.Path = requestOutputPath;
                }
                else
                {
                    context.Response.StatusCode = 404;
                    return;
                }
            }

            await _next(context);
        }

        protected bool IsImageRequested(string requestPath)
        {
            var requestExtension = Path.GetExtension(requestPath).ToLower();

            foreach (var extension in _extensions)
            {
                if (requestExtension.Contains(extension))
                {
                    return true;
                }
            }

            return false;
        }

        protected Size GetSize(IQueryCollection query, string widthArg = "w", string heightArg = "h")
        {
            if (query.Keys.Contains(widthArg))
            {
                if (query.Keys.Contains(heightArg))
                {
                    return new Size
                    {
                        Width = GetDimension(query, widthArg),
                        Height = GetDimension(query, heightArg)
                    };
                }

                return new Size { Width = GetDimension(query, widthArg) };
            }

            return default(Size);
        }

        protected int GetDimension(IQueryCollection query, string key)
            => int.Parse(query[key].First());
    }
}
