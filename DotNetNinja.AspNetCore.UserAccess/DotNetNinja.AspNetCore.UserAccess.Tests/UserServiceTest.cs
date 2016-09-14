using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace DotNetNinja.AspNetCore.UserAccess.Tests
{
    public class UserServiceTest
    {
        public UserServiceTest()
        {
            // http://blog.jongallant.com/2012/10/do-i-have-to-call-dispose-on-dbcontext.html - dbContext disposing
        }

        [Fact]
        public async Task UserService_CreateUserWithValidData_Succeed()
        {
            var testContext = new UserServiceTestContext();
            testContext.HashManagerMock
                .Setup(m => m.Hash(It.IsAny<string>()))
                .Returns(new PasswordSalt { Password = "hashedPass", Salt = "salt" })
                .Verifiable();

            var userService = testContext.CreateUserService();

            var login = "newUser";
            var pass = "pass";
            
            await userService.CreateUserAsync(login, pass);

            var created = await testContext.DbContext.Users.SingleAsync(u => u.Id == 1);

            testContext.HashManagerMock.Verify();
            Assert.Equal(created.Id, 1);
            Assert.Equal(created.Login, "newUser");
            Assert.Equal(created.Password, "hashedPass");
            Assert.Equal(created.Salt, "salt");
            Assert.True(string.IsNullOrEmpty(created.AccessToken));
            Assert.Equal(created.AccessTokenExpiration, default(DateTime));
        }

        public static IEnumerable<object[]> UserService_CreateUserWithAlreadyExistingLogin_ThrowsUserAccessException_Data()
        {
            yield return new[] { "busiedLogin" };
            yield return new[] { "BUSIEDLOGIN" };
            yield return new[] { "busiedlogin" };
        }

        [Theory, MemberData(nameof(UserService_CreateUserWithAlreadyExistingLogin_ThrowsUserAccessException_Data))]
        public async Task UserService_CreateUserWithAlreadyExistingLogin_ThrowsUserAccessException(string login)
        {
            var testContext = new UserServiceTestContext();
            testContext.DbContext.Users.Add(new User
            {
                Login = "busiedLogin"
            });
            await testContext.DbContext.SaveChangesAsync();

            var userService = testContext.CreateUserService();

            Func<Task> testCode = async () =>
            {
                await userService.CreateUserAsync(login, "pass");
            };

            await Assert.ThrowsAsync<UserAccessException>(testCode);
        }

        [Fact]
        public async Task UserService_CreateUserWithNullLogin_ThrowsArgumentNullException()
        {
            var testContext = new UserServiceTestContext();
            var userService = testContext.CreateUserService();

            Func<Task> testCode = async () =>
            {
                await userService.CreateUserAsync(null, "pass");
            };

            await Assert.ThrowsAsync<ArgumentNullException>(testCode);
        }

        public static IEnumerable<object[]> UserService_CreateUserWithEmptyLogin_ThrowsArgumentException_Data()
        {
            yield return new[] { "" };
            yield return new[] { " " };
            yield return new[] { "\n" };
            yield return new[] { "\t" };
        }

        [Theory, MemberData(nameof(UserService_CreateUserWithEmptyLogin_ThrowsArgumentException_Data))]
        public async Task UserService_CreateUserWithEmptyLogin_ThrowsArgumentException(string login)
        {
            var testContext = new UserServiceTestContext();
            var userService = testContext.CreateUserService();

            Func<Task> testCode = async () =>
            {
                await userService.CreateUserAsync(login, "pass");
            };
            
            await Assert.ThrowsAsync<ArgumentException>(testCode);
        }

        [Fact]
        public async Task UserService_CreateUserWithNullPassword_ThrowsArgumentNullException()
        {
            var testContext = new UserServiceTestContext();
            var userService = testContext.CreateUserService();

            Func<Task> testCode = async () =>
            {
                await userService.CreateUserAsync("login", null);
            };

            await Assert.ThrowsAsync<ArgumentNullException>(testCode);
        }
    }
}
