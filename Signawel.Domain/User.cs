using Microsoft.AspNetCore.Identity;

namespace Signawel.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
