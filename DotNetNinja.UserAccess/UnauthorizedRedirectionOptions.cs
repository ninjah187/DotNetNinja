using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.AspNetCore.UserAccess
{
    public class UnauthorizedRedirectionOptions
    {
        public UnauthorizedRedirectionOptions()
        {
        }

        public UnauthorizedRedirectionOptions(string routeValue)
        {
            RouteValue = routeValue;
        }

        public string RouteValue { get; set; }
    }
}
