using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ImageProcessorCore;

namespace DotNetNinja.AspNetCore.ImageResizer
{
    public class ImageResizer : IImageResizer
    {
        /// <summary>
        /// Dictionary where keys are image names and values are allowed sizes for specific image.
        /// </summary>
        Dictionary<string, List<Size>> _allowed;

        public ImageResizer(Dictionary<string, List<Size>> allowed)
        {
            _allowed = allowed;
        }

        public void ResizeAndSave(string inputPath, string outputPath, Size size)
        {
            if (!IsAllowed(inputPath, size))
            {
                return;
            }

            using (var inputStream = File.OpenRead(inputPath))
            using (var outputStream = File.OpenWrite(outputPath))
            {
                var image = new Image(inputStream);

                image
                    .Resize(size.Width.Value, size.Height ?? 0)
                    .Save(outputStream);
            }

            GC.Collect();
        }

        protected bool IsAllowed(string imageName, Size size)
            => _allowed == null || (_allowed.Keys.Contains(imageName) && _allowed[imageName].Contains(size));
    }
}
