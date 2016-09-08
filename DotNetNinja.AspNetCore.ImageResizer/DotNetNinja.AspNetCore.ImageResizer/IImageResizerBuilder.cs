using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.AspNetCore.ImageResizer
{
    public interface IImageResizerBuilder
    {
        IImageResizerBuilder Allow(Size size);
        IImageResizerBuilder Allow(string imageName);
        IImageResizerBuilder Allow(string imageName, Size size);
        IImageResizerBuilder AllowAll();

        IImageResizer Build();
    }
}
