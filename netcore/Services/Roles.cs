using Microsoft.AspNetCore.Identity;
using netcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Services
{
    public class Roles : IRoles
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Roles(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task UpdateRoles(ApplicationUser appUser,
            ApplicationUser currentLoginUser)
        {
            try
            {
                IList<string> roles = await _userManager.GetRolesAsync(appUser);
                foreach (var item in roles)
                {
                    await _userManager.RemoveFromRoleAsync(appUser, item);
                }
                /*

                if (appUser.isInRoleApplicationUser)
                {
                    if (!await _roleManager.RoleExistsAsync(netcore.MVC.Pages.ApplicationUser.Role))
                        await _roleManager.CreateAsync(new IdentityRole(netcore.MVC.Pages.ApplicationUser.Role));
                    await _userManager.AddToRoleAsync(appUser, netcore.MVC.Pages.ApplicationUser.Role);
                }

                if (appUser.isInRoleHomeAbout)
                {
                    if (!await _roleManager.RoleExistsAsync(netcore.MVC.Pages.HomeAbout.Role))
                        await _roleManager.CreateAsync(new IdentityRole(netcore.MVC.Pages.HomeAbout.Role));
                    await _userManager.AddToRoleAsync(appUser, netcore.MVC.Pages.HomeAbout.Role);
                }

                if (appUser.isInRoleHomeContact)
                {
                    if (!await _roleManager.RoleExistsAsync(netcore.MVC.Pages.HomeContact.Role))
                        await _roleManager.CreateAsync(new IdentityRole(netcore.MVC.Pages.HomeContact.Role));
                    await _userManager.AddToRoleAsync(appUser, netcore.MVC.Pages.HomeContact.Role);
                }

                if (appUser.isInRoleHomeIndex)
                {
                    if (!await _roleManager.RoleExistsAsync(netcore.MVC.Pages.HomeIndex.Role))
                        await _roleManager.CreateAsync(new IdentityRole(netcore.MVC.Pages.HomeIndex.Role));
                    await _userManager.AddToRoleAsync(appUser, netcore.MVC.Pages.HomeIndex.Role);
                }
                */
                

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
