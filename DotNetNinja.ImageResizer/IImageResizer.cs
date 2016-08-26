using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.ImageResizer
{
    public interface IImageResizer
    {
        void ResizeAndSave(string inputPath, string outputPath, Size size);
    }
}
