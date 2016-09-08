using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.AspNetCore.UserAccess
{
    /// <summary>
    /// Abstraction providing methods for hashing and hash verifying.
    /// </summary>
    public interface IHashManager
    {
        /// <summary>
        /// Produce hash from specified input with random generated salt.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PasswordSalt Hash(string input);

        /// <summary>
        /// Produce hash from specified input using specified salt.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        PasswordSalt Hash(string input, string salt);

        /// <summary>
        /// Produce hash from specified input using specified salt.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        PasswordSalt Hash(string input, byte[] salt);

        /// <summary>
        /// Compare inputHash hash with compareWith string using specified salt.
        /// </summary>
        /// <param name="inputHash"></param>
        /// <param name="compareWith"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        bool Verify(string inputHash, string compareWith, string salt);

        /// <summary>
        /// Compare inputHash hash with compareWith string using specified salt.
        /// </summary>
        /// <param name="inputHash"></param>
        /// <param name="compareWith"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        bool Verify(string inputHash, string compareWith, byte[] salt);
    }
}
