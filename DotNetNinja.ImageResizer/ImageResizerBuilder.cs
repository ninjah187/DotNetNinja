using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.AspNetCore.ImageResizer
{
    public class ImageResizerBuilder : IImageResizerBuilder
    {
        List<Size> _allowedSizes = new List<Size>();
        List<string> _allowedImages = new List<string>();

        Dictionary<string, List<Size>> _allowed = new Dictionary<string, List<Size>>();

        IImagePathGenerator _pathGenerator = new ImagePathGenerator();

        public IImageResizerBuilder Allow(Size size)
        {
            _allowedSizes?.Add(size);
            return this;
        }

        public IImageResizerBuilder Allow(string imageName)
        {
            _allowedImages?.Add(imageName);
            return this;
        }

        public IImageResizerBuilder Allow(string imageName, Size size)
        {
            if (_allowed == null)
            {
                return this;
            }

            if (_allowed.Keys.Contains(imageName))
            {
                var sizes = _allowed[imageName];
                if (!sizes.Contains(size))
                {
                    sizes.Add(size);
                }
            }
            else
            {
                _allowed.Add(imageName, new List<Size> { size });
            }

            return this;
        }

        public IImageResizerBuilder AllowAll()
        {
            _allowedSizes = null;
            _allowedImages = null;
            _allowed = null;
            return this;
        }

        public IImageResizer Build()
        {
            PopulateAllowedDictionary();

            return new ImageResizer(_allowed);
        }

        protected void PopulateAllowedDictionary()
        {
            if (_allowed == null)
            {
                return;
            }

            foreach (var image in _allowedImages)
            {
                if (_allowed.Keys.Contains(image))
                {
                    var sizes = _allowed[image];
                    foreach (var size in _allowedSizes)
                    {
                        if (!sizes.Contains(size))
                        {
                            sizes.Add(size);
                        }
                    }
                }
                else
                {
                    _allowed.Add(image, _allowedSizes.ToList());
                }
            }
        }

    }
}
