using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DotNetNinja.AspNetCore.UserAccess.Tests
{
    public class UserServiceTestContext
    {
        public TestDbContext DbContext { get; }
        public Mock<IHashManager> HashManagerMock { get; }
        public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; }

        public UserServiceTestContext()
        {
            DbContext = new TestDbContext(new DbContextOptionsBuilder()
                                                .UseInMemoryDatabase()
                                                .Options);
            HashManagerMock = new Mock<IHashManager>();
            HttpContextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        public UserService CreateUserService()
            => new UserService(DbContext, HashManagerMock.Object, HttpContextAccessorMock.Object);
    }
}
