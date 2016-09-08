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
    /// Filter attribute that restricts access to decorated controller/action only for authorized users.
    /// </summary>
    public class UserAccessAttribute : TypeFilterAttribute
    {
        public UserAccessAttribute()
            : base(typeof(UserAccessFilter))
        {
        }
    }
}
