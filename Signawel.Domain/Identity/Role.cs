using Microsoft.AspNetCore.Identity;

namespace Signawel.Domain
{
    public class Role : IdentityRole
    {
        public static class Constants
        {
            public const string Instance = "Instance";
            public const string Admin = "Admin";

            public static string[] Roles => new string[] { Instance, Admin };
        }
    }
}
