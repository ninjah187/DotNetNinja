using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace DotNetNinja.AspNetCore.UserAccess
{
    public static class UserAccessServiceCollectionExtensions
    {
        /// <summary>
        /// Adds singleton IHashManager service and scoped IUserService default implementations. Also adds scoped specific DbContext implementation.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddUserAccess<TDbContextImplementation>(this IServiceCollection services)
            where TDbContextImplementation : DbContext
            => services.AddUserAccess<RandomSaltGenerator, HashManager, UserService, TDbContextImplementation, HttpContextAccessor>();

        /// <summary>
        /// Adds singleton IHashManager service, scoped IUserService and scoped specific DbContext implementations.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddUserAccess<TSaltGenerator, THashManager, TUserService, TDbContextImplementation, THttpContextAccessor>(this IServiceCollection services)
            where TSaltGenerator : class, ISaltGenerator
            where THashManager : class, IHashManager
            where TUserService : class, IUserService
            where TDbContextImplementation : DbContext
            where THttpContextAccessor : class, IHttpContextAccessor
        {
            services
                .AddSingleton<ISaltGenerator, TSaltGenerator>()
                .AddSingleton<IHashManager, THashManager>()
                .AddSingleton<IHttpContextAccessor, THttpContextAccessor>()
                .AddScoped<IUserService, TUserService>();
            
            if (typeof(DbContext) != typeof(TDbContextImplementation))
            {
                services.AddScoped<DbContext, TDbContextImplementation>();
            }
            
            return services;
        }
    }
}
