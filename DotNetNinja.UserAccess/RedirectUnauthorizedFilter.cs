using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetNinja.UserAccess
{
    public class RedirectUnauthorizedFilter : IAsyncResultFilter
    {
        public string RedirectRoute { get; }

        public string ActionName { get; }
        public string ControllerName { get; }
        public object RouteValues { get; }
        public bool Permanent { get; }

        public RedirectUnauthorizedFilter(string actionName,
                                          string controllerName = null,
                                          object routeValues = null,
                                          bool permanent = false)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
            Permanent = permanent;
        }

        public RedirectUnauthorizedFilter(string redirectRoute)
        {
            RedirectRoute = redirectRoute;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
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
            else if (ActionName != null && ControllerName != null)
            {
                context.Result = new RedirectToActionResult(ActionName, ControllerName, RouteValues, Permanent);
            }
            await next();
        }
    }
}
