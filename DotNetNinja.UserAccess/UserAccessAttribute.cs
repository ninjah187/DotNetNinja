using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.UserAccess
{
    public class UserAccessAttribute : TypeFilterAttribute
    {
        public UserAccessAttribute()
            : base(typeof(UserAccessFilter))
        {
        }
    }
}
