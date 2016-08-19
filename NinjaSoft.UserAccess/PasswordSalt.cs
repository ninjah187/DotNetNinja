using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSoft.UserAccess
{
    /// <summary>
    /// Password and salt values pair.
    /// </summary>
    public struct PasswordSalt
    {
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
