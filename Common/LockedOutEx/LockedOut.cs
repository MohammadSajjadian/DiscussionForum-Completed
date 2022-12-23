using Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension.LockedOut
{
    public static class LockedOut
    {
        public static IServiceCollection LockedOutConfiguration(this IServiceCollection service)
        {
            service.AddIdentityCore<ApplicationUser>(x => { x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); });
            return service;
        }
    }
}
