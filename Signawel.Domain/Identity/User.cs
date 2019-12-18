using Microsoft.AspNetCore.Identity;
using Signawel.Domain.ReportGroups;
using System.Collections.Generic;

namespace Signawel.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserReportGroup> UserReportGroups { get; set; }
    }
}
