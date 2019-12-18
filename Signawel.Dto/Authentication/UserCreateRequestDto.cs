using System.Collections.Generic;

namespace Signawel.Dto.Authentication
{
    public class UserCreateRequestDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
