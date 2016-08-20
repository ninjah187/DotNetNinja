using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NinjaSoft.UserAccess
{
    /// <summary>
    /// Controller providing log in and log out functionality.
    /// </summary>
    [Route("users")]
    public class UserAccessController : Controller
    {
        protected IUserService UserService { get; }

        public UserAccessController(IUserService userService)
        {
            UserService = userService;
        }

        [Route("login")]
        public virtual IActionResult LogIn()
            => View("Views/Users/Login.cshtml");

        [HttpPost]
        [Route("login")]
        public virtual async Task<IActionResult> LogIn(string login, string password)
        {
            var user = await UserService.GetUserAsync(login);
            
            if (user == null)
            {
                // later - view about not being logged
                return NotFound();
            }

            var token = await UserService.LogInAsync(user, password);

            if (token == null)
            {
                return Unauthorized();
            }

            return RedirectToAction("Index", "Pages");
        }
    }
}
