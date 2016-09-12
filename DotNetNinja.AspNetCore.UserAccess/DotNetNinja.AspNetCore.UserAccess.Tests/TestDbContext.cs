using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.AspNetCore.UserAccess.Tests
{
    public class TestDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public TestDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
