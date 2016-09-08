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
        public static IEnumerable<object[]> GenerateResizedPathData
        {
            get
            {
                return new[]
                {
                    new object[] { "image.jpg", 100, 100, "image-100x100.jpg" },
                    new object[] { "image.jpg", 100, null, "image-100x.jpg" },
                    new object[] { "image.jpg", null, 100, "image-x100.jpg" },
                    new object[] { @"C:/images/image.jpg", 100, 100, @"C:/images/image-100x100.jpg" },
                    new object[] { @"C:/images/image.jpg", 100, null, @"C:/images/image-100x.jpg" },
                    new object[] { @"C:/images/image.jpg", null, 100, @"C:/images/image-x100.jpg" },
                    new object[] { @"/var/images/image.jpg", 100, 100, @"/var/images/image-100x100.jpg" },
                    new object[] { @"/var/images/image.jpg", 100, null, @"/var/images/image-100x.jpg" },
                    new object[] { @"/var/images/image.jpg", null, 100, @"/var/images/image-x100.jpg" },
                };
            }
        }

        [Theory, MemberData(nameof(GenerateResizedPathData))]
        public void GenerateResizedPath(string inputPath, int? width, int? height, string expectedOutput)
        {
            var desiredSize = new Size(width, height);
            var generator = new ImagePathGenerator();

            var output = generator.GetResizedPath(inputPath, desiredSize);
            
            Assert.Equal(expectedOutput, output);
        }

        public static IEnumerable<object[]> GenerateResizedPathThrowingSizeExceptionData()
        {
            yield return new object[] { "image.jpg", null, null, "image.jpg" };
            yield return new object[] { "image.jpg", 0, 0, "image.jpg" };
            yield return new object[] { "image.jpg", 0, null, "image.jpg" };
            yield return new object[] { "image.jpg", null, 0, "image.jpg" };
        }

        [Theory, MemberData(nameof(GenerateResizedPathThrowingSizeExceptionData))]
        public void GenerateResizedPathThrowingSizeException(string inputPath, int? width, int? height, string expectedOutput)
        {
            var generator = new ImagePathGenerator();
            var desiredSize = new Size(width, height);

            Action testCode = () =>
            {
                var output = generator.GetResizedPath(inputPath, desiredSize);
            };

            Assert.Throws<ArgumentException>(testCode);
        }

        public static IEnumerable<object[]> GenerateResizedPathThrowingEmptyStringExceptionData()
        {
            yield return new object[] { "", 100, 100, "image-100x100.jpg" };
            yield return new object[] { " ", 100, 100, "image-100x100.jpg" };
            yield return new object[] { "   ", 100, 100, "image-100x100.jpg" };
            yield return new object[] { "   ", 100, 100, "image-100x100.jpg" };
        }

        [Theory, MemberData(nameof(GenerateResizedPathThrowingEmptyStringExceptionData))]
        public void GenerateResizedPathThrowingEmptyStringException(string inputPath, int? width, int? height, string expectedOutput)
        {
            var generator = new ImagePathGenerator();
            var size = new Size(width, height);

            Action testCode = () =>
            {
                var output = generator.GetResizedPath(inputPath, size);
            };

            Assert.Throws<ArgumentException>(testCode);
        }

        [Fact]
        public void GenerateResizedPathThrowingNullStringException()
        {
            var generator = new ImagePathGenerator();
            var size = new Size(100, 100);

            Action testCode = () =>
            {
                var output = generator.GetResizedPath(null, size);
            };

            Assert.Throws<ArgumentNullException>(testCode);
        }

        public static IEnumerable<object[]> GenerateOriginalPathData()
        {
            yield return new[] { "image-100x100.jpg", "image.jpg" };
            yield return new[] { "image-100x.jpg", "image.jpg" };
            yield return new[] { "image-x100.jpg", "image.jpg" };
            yield return new[] { @"C:/images/image-100x100.jpg", @"C:/images/image.jpg" };
            yield return new[] { @"C:/images/image-100x.jpg", @"C:/images/image.jpg" };
            yield return new[] { @"C:/images/image-x100.jpg", @"C:/images/image.jpg" };
            yield return new[] { @"/var/images/image-100x100.jpg", @"/var/images/image.jpg" };
            yield return new[] { @"/var/images/image-100x.jpg", @"/var/images/image.jpg" };
            yield return new[] { @"/var/images/image-x100.jpg", @"/var/images/image.jpg" };
        }

        [Theory, MemberData(nameof(GenerateOriginalPathData))]
        public void GenerateOriginalPath(string inputPath, string expectedOutput)
        {
            var generator = new ImagePathGenerator();

            Assert.Equal(expectedOutput, generator.GetOriginalPath(inputPath));
        }

        public static IEnumerable<object[]> GenerateOriginalPathThrowingEmptyStringExceptionData()
        {
            yield return new[] { "" };
            yield return new[] { " " };
            yield return new[] { "  " };
            yield return new[] { "  " };
        }

        [Theory, MemberData(nameof(GenerateOriginalPathThrowingEmptyStringExceptionData))]
        public void GenerateOriginalPathThrowingEmptyStringException(string resizedPath)
        {
            var generator = new ImagePathGenerator();

            Action testCode = () =>
            {
                var output = generator.GetOriginalPath(resizedPath);
            };

            Assert.Throws<ArgumentException>(testCode);
        }

        [Fact]
        public void GenerateOriginalPathThrowingNullStringException()
        {
            var generator = new ImagePathGenerator();

            Action testCode = () =>
            {
                var output = generator.GetOriginalPath(null);
            };

            Assert.Throws<ArgumentNullException>(testCode);
        }
    }
}
