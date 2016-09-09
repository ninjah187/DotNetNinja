using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DotNetNinja.AspNetCore.UserAccess;

namespace DotNetNinja.AspNetCore.UserAccess.Tests
{
    public class HashManagerTest
    {
        HashManager _hashManager;

        public HashManagerTest()
        {
            var saltGeneratorMock = new Mock<ISaltGenerator>();

            saltGeneratorMock
                .Setup(gen => gen.GetSalt(16))
                .Returns(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf });

            _hashManager = new HashManager(saltGeneratorMock.Object);
        }

        public static IEnumerable<object[]> HashingData()
        {
            yield return new[] { "",                         "YjDnC9pxMzZBmiNsT2/T1DMyMz7U/MTquEKBVemJE5Q=" };
            yield return new[] { "password",                 "jj4vc8PrY5CoGrvIEBwDQ7AXp6//+1q2XhNPCQncyiw=" };
            yield return new[] { "PASSWORD",                 "7qp/L5CybIdFpEinezzk/KBJmfZIw4m8gP+xoazptBI=" };
            yield return new[] { "ąćęńóżź",                  "wvnZ6bi0f3vooWGdwEo4rMzp8O6u0LfvWTknYm1rrZw=" };
            yield return new[] { "ĄĆĘŃÓŻŹ",                  "vb3MxvrDeGxfuw4AOrqVWsTDW/LYZEP/5bmtJAHmjyU=" };
            yield return new[] { "123456",                   "b08tYhhCvN9SxE73PSjWAXBF5dVDtVR9Vt3aFSTQPOE=" };
            yield return new[] { @"!@#$%^&*()_=-/?.,;'\][~", "qtYSXnRahjeYfyNn4GXHxrlv8CV1yivjojLJNhJ6dZg=" };
            yield return new[] { "sDjQ1NąŹ87#*$",            "LTxKIIyvqRwkg582iLc26hoWo9c46rl4DrdR0RL4pKw=" };
        }

        [Theory, MemberData(nameof(HashingData))]
        public void Hashing(string input, string expected)
        {
            var result = _hashManager.Hash(input);

            Assert.Equal(expected, result.Password);
        }

        public static IEnumerable<object[]> HashingWithSaltData()
        {
            // Func<> instead of byte[] to ensure immutability
            Func<byte[]> salt = () => new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };

            yield return new object[] { "",                         salt(), "YjDnC9pxMzZBmiNsT2/T1DMyMz7U/MTquEKBVemJE5Q=" };
            yield return new object[] { "password",                 salt(), "jj4vc8PrY5CoGrvIEBwDQ7AXp6//+1q2XhNPCQncyiw=" };
            yield return new object[] { "PASSWORD",                 salt(), "7qp/L5CybIdFpEinezzk/KBJmfZIw4m8gP+xoazptBI=" };
            yield return new object[] { "ąćęńóżź",                  salt(), "wvnZ6bi0f3vooWGdwEo4rMzp8O6u0LfvWTknYm1rrZw=" };
            yield return new object[] { "ĄĆĘŃÓŻŹ",                  salt(), "vb3MxvrDeGxfuw4AOrqVWsTDW/LYZEP/5bmtJAHmjyU=" };
            yield return new object[] { "123456",                   salt(), "b08tYhhCvN9SxE73PSjWAXBF5dVDtVR9Vt3aFSTQPOE=" };
            yield return new object[] { @"!@#$%^&*()_=-/?.,;'\][~", salt(), "qtYSXnRahjeYfyNn4GXHxrlv8CV1yivjojLJNhJ6dZg=" };
            yield return new object[] { "sDjQ1NąŹ87#*$",            salt(), "LTxKIIyvqRwkg582iLc26hoWo9c46rl4DrdR0RL4pKw=" };
        }

        [Theory, MemberData(nameof(HashingWithSaltData))]
        public void HashingWithSaltBytes(string input, byte[] salt, string expected)
        {
            var result = _hashManager.Hash(input, salt);

            Assert.Equal(expected, result.Password);
        }

        [Theory, MemberData(nameof(HashingWithSaltData))]
        public void HashingWithSaltString(string input, byte[] saltBytes, string expected)
        {
            var salt = Convert.ToBase64String(saltBytes);

            var result = _hashManager.Hash(input, salt);

            Assert.Equal(expected, result.Password);
        }

        [Fact]
        public void ArgumentNullExceptionWhenHashInputIsNull()
        {
            Action testCode = () =>
            {
                var output = _hashManager.Hash(null);
            };

            Assert.Throws<ArgumentNullException>(testCode);
        }

        [Theory, MemberData(nameof(HashingWithSaltData))]
        public void VerifyingWithSaltBytes(string compareWith, byte[] salt, string inputHash)
        {
            Assert.True(_hashManager.Verify(inputHash, compareWith, salt));
        }

        [Theory, MemberData(nameof(HashingWithSaltData))]
        public void VerifyingWithSaltString(string compareWith, byte[] saltBytes, string inputHash)
        {
            var salt = Convert.ToBase64String(saltBytes);

            Assert.True(_hashManager.Verify(inputHash, compareWith, salt));
        }
    }
}
