using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace netcore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string profilePictureUrl { get; set; } = "/images/empty_profile.png";
        public bool isSuperAdmin { get; set; } = false;

        //roles
        public bool isInRoleHomeIndex { get; set; } = false;
        public bool isInRoleHomeAbout { get; set; } = false;
        public bool isInRoleHomeContact { get; set; } = false;

        public bool isInRoleApplicationUser { get; set; } = false;
    }
}
