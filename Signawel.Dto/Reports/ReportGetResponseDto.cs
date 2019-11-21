using Signawel.Domain;
using System.Collections.Generic;

namespace Signawel.Dto.Reports
{
    public class ReportGetResponseDto
    {
        public string Id { get; set; }

        public string UserEmail { get; set; }

        public string CustomMessage { get; set; }

        public string RoadWorkId { get; set; }

        public ICollection<string> Cities { get; set; }

        public string Description { get; set; }

        public ICollection<ReportImage> Images { get; set; }

        public ICollection<ReportIssue> IssueLink { get; set; }
    }
}
