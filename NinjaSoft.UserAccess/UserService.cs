using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NinjaSoft.UserAccess
{
    /// <summary>
    /// IUserService working with Entity Framework Core.
    /// </summary>
    public class UserService : IUserService
    {
        DbContext _dbContext;
        IHashManager _hashManager;

        public UserService(DbContext dbContext, IHashManager hashManager)
        {
            _dbContext = dbContext;
            _hashManager = hashManager;
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
            var guidToken = new Guid(token);

            var user = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(u => u.AccessToken == guidToken
                                        && u.AccessTokenExpiration >= DateTime.Now);

            return user;
        }

        public async Task<User> GetUserAsync(string login)
            => await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Login.Equals(login, StringComparison.CurrentCultureIgnoreCase));

        public async Task<Guid?> LogInAsync(User user, string password)
        {
            if (user == null)
            {
                return null;
            }

            if (!VerifyPassword(user, password))
            {
                return null;
            }

            user.AccessToken = Guid.NewGuid();
            user.AccessTokenExpiration = DateTime.Now + TimeSpan.FromMinutes(30);

            await _dbContext.SaveChangesAsync();

            return user.AccessToken;
        }

        public async Task<Guid?> LogInAsync(string login, string password)
        {
            var user = await GetUserAsync(login);

            return await LogInAsync(user, password);
        }

        public async Task<bool> VerifyAsync(string token)
            => await GetLoggedUserAsync(token) != null;

        public bool VerifyPassword(User user, string password)
            => _hashManager.Verify(user.Password, password, user.Salt);
    }
}
