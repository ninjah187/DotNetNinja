using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DotNetNinja.AspNetCore.ImageResizer
{
    public class ImagePathGenerator : IImagePathGenerator
    {
        public string GetResizedPath(string originalPath, Size size)
        {
            var fileName = Path.GetFileNameWithoutExtension(originalPath);
            var extension = Path.GetExtension(originalPath);

            var resizedFile = $"{fileName}-{size.Width}x{size.Height}{extension}";

            return originalPath.Replace(Path.GetFileName(originalPath), resizedFile);
        }

        public string GetOriginalPath(string resizedPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(resizedPath);
            var extension = Path.GetExtension(resizedPath);

            var originalFile = "";

            var split = fileName.Split('-');

            foreach (var str in split)
            {
                if (object.ReferenceEquals(str, split.Last()))
                {
                    continue;
                }

                originalFile += str;
            }

            originalFile += extension;

            return resizedPath.Replace(Path.GetFileName(resizedPath), originalFile);
        }
    }
}
