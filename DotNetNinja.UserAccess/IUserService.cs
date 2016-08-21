using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.UserAccess
{
    /// <summary>
    /// Service providing methods for users accounts management, authentication and authorization.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates new user with given credentials if login isn't already occupied.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>User object representing created user.</returns>
        Task<User> CreateUserAsync(string login, string password);

        /// <summary>
        /// Indicates whether specified access token is valid token of logged user.
        /// </summary>
        /// <param name="token">User access token.</param>
        Task<bool> VerifyAsync(string token);

        /// <summary>
        /// Attempts to log in user with given credentials. If succeed, returns access token, otherwise returns null.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>User access token if succeed, null otherwise.</returns>
        Task<string> LogInAsync(string login, string password);

        /// <summary>
        /// Attempts to log in user with given credentials. If succeed, returns access token, otherwise returns null.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <param name="password">User password.</param>
        /// <returns>User access token if succeed, null otherwise.</returns>
        Task<string> LogInAsync(User user, string password);

        /// <summary>
        /// Gets user with specified login.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <returns>User object.</returns>
        Task<User> GetUserAsync(string login);

        /// <summary>
        /// Gets user logged in with specific access token. Returns null if there's no such user or token is expired.
        /// </summary>
        /// <param name="token">User access token.</param>
        /// <returns>User object.</returns>
        Task<User> GetLoggedUserAsync(string token);

        /// <summary>
        /// Indicates whether given password matches the user password.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool VerifyPassword(User user, string password);
    }
}
