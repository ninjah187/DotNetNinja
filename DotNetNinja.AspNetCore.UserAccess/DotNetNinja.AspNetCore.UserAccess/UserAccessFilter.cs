using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.AspNetCore.UserAccess
{
    /// <summary>
    /// Filter that performs user access authorization.
    /// </summary>
    public class UserAccessFilter : IAsyncAuthorizationFilter
    {
        IUserService _userService;

        public UserAccessFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!await _userService.VerifyAsync())
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
