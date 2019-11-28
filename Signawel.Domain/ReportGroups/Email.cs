using System.Collections.Generic;

namespace Signawel.Domain.ReportGroups
{
    public class Email : Entity
    {
        public string EmailAddress { get; set; }
        public virtual List<EmailReportGroup> EmailReportGroups { get; set; }
    }
}
