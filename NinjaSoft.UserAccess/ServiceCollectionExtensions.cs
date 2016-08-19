using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace NinjaSoft.UserAccess
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds singleton IHashManager service and scoped IUserService implementations.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddUserAccessServices(this IServiceCollection services)
            => services.AddUserAccessServices<HashManager, UserService>();

        /// <summary>
        /// Adds singleton IHashManager service and scoped IUserService implementations.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddUserAccessServices<THashManager, TUserService>(this IServiceCollection services)
            where THashManager : class, IHashManager
            where TUserService : class, IUserService
        {
            services
                .AddSingleton<IHashManager, THashManager>()
                .AddScoped<IUserService, TUserService>();

            return services;
        }
    }
}
