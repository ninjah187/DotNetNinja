using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.AspNetCore.UserAccess
{
    /// <summary>
    /// Service used to obtain salt for password hashing.
    /// </summary>
    public interface ISaltGenerator
    {
        /// <summary>
        /// Generate salt of specific length.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        byte[] GetSalt(int length);
    }
}
