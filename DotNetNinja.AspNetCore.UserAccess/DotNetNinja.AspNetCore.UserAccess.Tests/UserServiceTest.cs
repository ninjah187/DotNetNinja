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
        IUserService _userService;

        public UserServiceTest()
        {
            // http://blog.jongallant.com/2012/10/do-i-have-to-call-dispose-on-dbcontext.html - dbContext disposing
        }

        [Fact]
        public async Task UserService_CreateUserWithValidData_Succeed()
        {
            var dbContext = new TestDbContext(new DbContextOptionsBuilder()
                                                .UseInMemoryDatabase()
                                                .Options);

            var hashManagerMock = new Mock<IHashManager>();
            hashManagerMock.Setup(m => m.Hash(It.IsAny<string>())).Returns(new PasswordSalt { Password = "hashedPass", Salt = "salt" });

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var userService = new UserService(dbContext, hashManagerMock.Object, httpContextAccessorMock.Object);

            var login = "newUser";
            var pass = "pass";

            //var created = await userService.CreateUserAsync(login, pass);
            await userService.CreateUserAsync(login, pass);

            var created = await dbContext.Users.SingleAsync(u => u.Id == 1);

            Assert.Equal(created.Id, 1);
            Assert.Equal(created.Login, "newUser");
            Assert.Equal(created.Password, "hashedPass");
            Assert.Equal(created.Salt, "salt");
            Assert.True(string.IsNullOrEmpty(created.AccessToken));
            Assert.Equal(created.AccessTokenExpiration, default(DateTime));
        }
    }
}
