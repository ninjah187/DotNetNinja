﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.ImageResizer
{
    public interface IImagePathGenerator
    {
        string GetResizedPath(string originalPath, Size size);
        string GetOriginalPath(string resizedPath);
    }
}
