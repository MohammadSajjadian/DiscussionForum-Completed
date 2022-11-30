using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension.AccessLevelsPathEx
{
    public static class AccessLevelPath
    {
        public static IServiceCollection AccessLevelPathConfiguration(this IServiceCollection service)
        {
            CookieAuthenticationOptions options = new() { LoginPath = "Index/Login", AccessDeniedPath = "Index/Login" };
            return service;
        }
    }
}
