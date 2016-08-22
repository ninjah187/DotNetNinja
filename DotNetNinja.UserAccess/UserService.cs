using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.UserAccess
{
    /// <summary>
    /// Default IUserService using Entity Framework Core and cookies.
    /// </summary>
    public class UserService : IUserService
    {
        DbContext _dbContext;
        IHashManager _hashManager;
        HttpContext _httpContext;

        public UserService(DbContext dbContext, IHashManager hashManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _hashManager = hashManager;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<User> CreateUserAsync(string login, string password)
        {
            if (await _dbContext.Set<User>()
                    .AnyAsync(u => u.Login.Equals(login, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new UserAccessException($"User creation error. User {login} already exists in database.");
            }

            var hash = _hashManager.Hash(password);

            var user = new User
            {
                Login = login,
                Password = hash.Password,
                Salt = hash.Salt
            };

            _dbContext.Set<User>().Add(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetLoggedUserAsync(string token)
        {
            var user = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(u => u.AccessToken == token
                                        && u.AccessTokenExpiration >= DateTime.Now);

            return user;
        }

        public async Task<User> GetUserAsync(string login)
            => await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Login.Equals(login, StringComparison.CurrentCultureIgnoreCase));

        /// <summary>
        /// If password matches user, creates access token in database and cookie.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> LogInAsync(User user, string password)
        {
            if (user == null)
            {
                return null;
            }

            if (!VerifyPassword(user, password))
            {
                return null;
            }

            //await CreateNewOrSustainExistingToken(user);
            await CreateNewTokenAsync(user);

            return user.AccessToken;
        }

        public async Task LogOutAsync(User user)
        {
            if (user == null)
            {
                return;
            }

            await DeleteTokenAsync(user);
        }

        public async Task CreateNewTokenAsync(User user)
        {
            user.AccessToken = _hashManager.Hash(Guid.NewGuid().ToString()).Password;
            user.AccessTokenExpiration = DateTime.Now + TimeSpan.FromMinutes(30);

            await _dbContext.SaveChangesAsync();

            _httpContext.Response.Cookies.Append("accessToken", user.AccessToken, new CookieOptions
            {
                Expires = user.AccessTokenExpiration,
                HttpOnly = true,
                //Secure = true
            });
        }

        public async Task DeleteTokenAsync(User user)
        {
            user.AccessTokenExpiration = DateTime.Now - TimeSpan.FromMinutes(1);

            await _dbContext.SaveChangesAsync();

            if (_httpContext.Request.Cookies.ContainsKey("accessToken"))
            {
                _httpContext.Response.Cookies.Delete("accessToken", new CookieOptions
                {
                    HttpOnly = true
                });
            }
        }

        public async Task<string> LogInAsync(string login, string password)
        {
            var user = await GetUserAsync(login);
            
            return await LogInAsync(user, password);
        }

        public async Task LogOutAsync(string token)
        {
            await LogOutAsync(await GetLoggedUserAsync(token));
        }

        public async Task<bool> VerifyAsync(string token)
            => await GetLoggedUserAsync(token) != null;

        public bool VerifyPassword(User user, string password)
            => _hashManager.Verify(user.Password, password, user.Salt);
    }
}
