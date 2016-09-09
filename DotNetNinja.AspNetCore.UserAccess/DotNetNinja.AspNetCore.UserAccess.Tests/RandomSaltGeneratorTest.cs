using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DotNetNinja.AspNetCore.UserAccess;

namespace DotNetNinja.AspNetCore.UserAccess.Tests
{
    public class RandomSaltGeneratorTest
    {
        public static IEnumerable<object[]> ArgumentExceptionWhenLengthLessOrEqualsZeroData()
        {
            yield return new object[] { -100 };
            yield return new object[] { -1 };
            yield return new object[] { 0 };
        }

        [Theory]
        [MemberData(nameof(ArgumentExceptionWhenLengthLessOrEqualsZeroData))]
        public void ArgumentExceptionWhenLengthLessOrEqualsZero(int length)
        {
            var generator = new RandomSaltGenerator();

            Action testCode = () =>
            {
                var salt = generator.GetSalt(length);
            };

            Assert.Throws<ArgumentException>(testCode);
        }

        public static IEnumerable<object[]> LengthCheckAfterSuccessfulGenerationData()
        {
            yield return new object[] { 128 / 8 };
            yield return new object[] { 1 };
            yield return new object[] { 1024 };
        }

        [Theory, MemberData(nameof(LengthCheckAfterSuccessfulGenerationData))]
        public void LengthCheckAfterSuccessfulGeneration(int length)
        {
            var generator = new RandomSaltGenerator();

            var salt = generator.GetSalt(length);

            Assert.Equal(length, salt.Length);
        }
    }
}
