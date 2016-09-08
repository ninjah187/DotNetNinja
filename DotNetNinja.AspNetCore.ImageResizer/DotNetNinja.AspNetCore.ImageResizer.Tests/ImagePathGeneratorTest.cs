using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DotNetNinja.AspNetCore.ImageResizer;

namespace DotNetNinja.AspNetCore.ImageResizer.Tests
{
    public class ImagePathGeneratorTest
    {
        public static IEnumerable<object> GenerateResizedPathData
        {
            get
            {
                return new[]
                {
                    new object[] { "image.jpg", new Size { Width = 100, Height = 100 }, "image-100x100.jpg" }
                };
            }
        }

        [Theory, MemberData(nameof(GenerateResizedPathData))]
        public void GenerateResizedPath(string inputPath, Size desiredSize, string expectedOutput)
        {
            var generator = new ImagePathGenerator();

            var output = generator.GetResizedPath(inputPath, desiredSize);

            Assert.Equal(expectedOutput, output);
        }
    }
}
