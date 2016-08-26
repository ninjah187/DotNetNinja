using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.ImageResizer
{
    public struct Size
    {
        public static Size All = new Size(int.MinValue, int.MinValue);

        public Size(int width)
            : this(width, null)
        {
        }

        public Size(int? width, int? height)
        {
            Width = width;
            Height = height;
        }

        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
