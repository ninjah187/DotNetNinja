using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.UserAccess
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Verifies whether connected peer is logged user with access to current action.
        /// </summary>
        public static async Task<bool> VerifyAccessAsync(this Controller controller)
        {
            if (!controller.HttpContext.Request.Cookies.ContainsKey("accessToken"))
            {
                return false;
            }

            var token = controller.HttpContext.Request.Cookies["accessToken"];

            var userService = (IUserService) controller.HttpContext.RequestServices.GetService(typeof(IUserService));

            return await userService.VerifyAsync(token);
        }
    }
}
