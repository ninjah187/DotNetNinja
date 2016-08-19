using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSoft.UserAccess
{
    /// <summary>
    /// Exception occuring on errors related with NinjaSoft.UserAccess.
    /// </summary>
    public class UserAccessException : Exception
    {
        public User User { get; }

        public UserAccessException()
        {
        }

        public UserAccessException(string message)
            : base(message)
        {
        }

        public UserAccessException(User user)
            : base($"User access exception: {user.Login} (id: {user.Id})")
        {
        }

        public UserAccessException(string message, User user)
            : this(message)
        {
            User = user;
        }
    }
}
