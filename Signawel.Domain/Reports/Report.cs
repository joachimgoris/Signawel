using System;
using System.Collections.Generic;

namespace Signawel.Domain
{
    public class Report
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserEmail { get; set; }

        public string CustomMessage { get; set; }

        public bool Priority { get; set; }

        public ICollection<ReportImage> Images { get; set; }

        public ICollection<ReportIssue> IssueLink { get; set; }
    }
}
