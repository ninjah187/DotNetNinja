using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSoft.UserAccess
{
    /// <summary>
    /// Represantation of user.
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public Guid AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}
