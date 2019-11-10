using System.Collections.Generic;

namespace Signawel.Domain.Authentication.Models
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
