using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetNinja.UserAccess
{
    class RedirectUnauthorizedAttribute : ResultFilterAttribute
    {
        public string RedirectRoute { get; set; }

        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public object RouteValues { get; set; }
        public bool Permanent { get; set; }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!(context.Result is UnauthorizedResult))
            {
                await next();
                return;
            }

            if (RedirectRoute != null)
            {
                context.Result = new RedirectToRouteResult(RedirectRoute);
            }
            else if (ActionName != null)
            {
                context.Result = new RedirectToActionResult(ActionName, ControllerName, RouteValues, Permanent);
            }
            else
            {
                throw new InvalidOperationException("RedirectRoute or ActionName parameters are needed.");
            }

            await next();
        }
    }
}
