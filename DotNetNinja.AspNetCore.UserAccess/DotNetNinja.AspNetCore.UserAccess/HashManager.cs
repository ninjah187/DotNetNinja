using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DotNetNinja.AspNetCore.UserAccess
{
    /// <summary>
    /// Object providing methods for Pbkdf2 hash managing and verifying.
    /// </summary>
    public class HashManager : IHashManager
    {
        ISaltGenerator _saltGenerator;

        public HashManager(ISaltGenerator saltGenerator)
        {
            _saltGenerator = saltGenerator;
        }

        public PasswordSalt Hash(string input)
        {
            var salt = _saltGenerator.GetSalt(128 / 8); // generate 128-bit salt

            return Hash(input, salt);
        }

        public PasswordSalt Hash(string input, string salt)
        {
            return Hash(input, Convert.FromBase64String(salt));
        }

        public PasswordSalt Hash(string input, byte[] salt)
        {
            var hash = KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return new PasswordSalt
            {
                Password = Convert.ToBase64String(hash),
                Salt = Convert.ToBase64String(salt)
            };
        }

        public bool Verify(string inputHash, string compareWith, string salt)
            => Verify(inputHash, compareWith, Convert.FromBase64String(salt));

        public bool Verify(string inputHash, string compareWith, byte[] salt)
            => Hash(compareWith, salt).Password == inputHash;
    }
}
