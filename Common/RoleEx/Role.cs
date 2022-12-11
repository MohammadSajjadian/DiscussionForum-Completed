using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension.RoleEx
{
    public static class Role
    {
        private readonly static string adminUserName = "2222h.com@gmail.com";
        private readonly static string adminPassword = "pP_0987";

        public static async Task<IServiceCollection> CreateRole(this IServiceCollection service, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = new() { "admin", "user" };
            foreach (var roleName in roles)
            {
                IdentityRole role = new(roleName);
                await roleManager.CreateAsync(role);
            }
            await FindAdmin(userManager);

            return service;
        }

        public static async Task FindAdmin(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = await userManager.FindByNameAsync(adminUserName);
            await CreateAdmin(userManager, user);
        }

        public static async Task CreateAdmin(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            if (user == null)
            {
                user = new() { UserName = adminUserName, Email = adminUserName, nickName = "admin", EmailConfirmed = true };
                await userManager.CreateAsync(user, adminPassword);
            }
            await AddAdminToAdminRole(userManager, user);
        }

        public static async Task AddAdminToAdminRole(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            if (await userManager.IsInRoleAsync(user, "admin") == false) await userManager.AddToRoleAsync(user, "admin");
        }
    }
}
