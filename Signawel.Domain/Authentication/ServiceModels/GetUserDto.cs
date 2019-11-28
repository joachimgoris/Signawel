﻿using System;
using System.Collections.Generic;

namespace Signawel.Domain.Authentication.Models
{
    public class GetUserDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string PhoneNumber { get; set; }
    }
}
