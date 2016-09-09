using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DotNetNinja.AspNetCore.UserAccess
{
    /// <summary>
    /// Cryptographically safe random salt generator.
    /// </summary>
    public class RandomSaltGenerator : ISaltGenerator
    {
        /// <summary>
        /// Generates random, cryptographically safe salt which consists of 'length' bytes.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetSalt(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Salt's length is <= 0.", nameof(length));
            }

            var salt = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
