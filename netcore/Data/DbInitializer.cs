using Microsoft.AspNetCore.Identity;
using netcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace netcore.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            //check for users
            if (context.ApplicationUser.Any())
            {
                return; //if user is not empty, DB has been seed
            }

            //init app with super admin user
            ApplicationUser superAdmin = new ApplicationUser();
            superAdmin.Email = "super@admin.com";
            superAdmin.UserName = superAdmin.Email;
            superAdmin.EmailConfirmed = true;
            superAdmin.isSuperAdmin = true;
            superAdmin.isInRoleApplicationUser = true;
            superAdmin.isInRoleHomeAbout = true;
            superAdmin.isInRoleHomeContact = true;
            superAdmin.isInRoleHomeIndex = true;

            await userManager.CreateAsync(superAdmin, "123456");

            foreach (var item in typeof(netcore.MVC.Pages).GetNestedTypes())
            {
                var roleName = item.Name;
                if (!await roleManager.RoleExistsAsync(roleName)) { await roleManager.CreateAsync(new IdentityRole(roleName)); }
                
                await userManager.AddToRoleAsync(superAdmin, roleName);
            }
            
        }
    }
}
