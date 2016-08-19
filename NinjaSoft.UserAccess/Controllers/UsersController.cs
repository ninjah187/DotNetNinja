﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NinjaSoft.UserAccess
{
    [Route("users")]
    public class UsersController : Controller
    {
        protected IUserService UserService { get; }

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        [Route("login")]
        public IActionResult LogIn()
            => View();

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(string login, string password)
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

            HttpContext.Response.Cookies.Append("accessToken", token.ToString(), new CookieOptions
            {
                Expires = DateTime.Now + TimeSpan.FromMinutes(30),
                //HttpOnly = true,
                //Secure = true
            });

            return RedirectToAction("Index", "Home");
        }
    }
}
