using System;
using System.Collections.Generic;

namespace Signawel.Domain.Reports
{
    public class Report : Entity
    {
        public DateTime CreationTime { get; set; }

        public string SenderEmail { get; set; }

        public string Description { get; set; }

        public string RoadworkId { get; set; }

        public ICollection<ReportImage> Images { get; set; }

        public string IssueId { get; set; }

        public string Cities { get; set; }
    }
}
