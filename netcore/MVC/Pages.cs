using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.MVC
{
    public static class Pages
    {
        public static class HomeIndex
        {
            public const string Controller = "Home";
            public const string Action = "Index";
            public const string Role = "HomeIndex";
            public const string Url = "/Home/Index";
        }

        public static class HomeAbout
        {
            public const string Controller = "Home";
            public const string Action = "About";
            public const string Role = "HomeAbout";
            public const string Url = "/Home/About";
        }

        public static class HomeContact
        {
            public const string Controller = "Home";
            public const string Action = "Contact";
            public const string Role = "HomeContact";
            public const string Url = "/Home/Contact";
        }

        public static class ApplicationUser
        {
            public const string Controller = "ApplicationUser";
            public const string Action = "Index";
            public const string Role = "ApplicationUser";
            public const string Url = "/ApplicationUser/Index";
        }
    }
}
