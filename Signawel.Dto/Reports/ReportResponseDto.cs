using System;
using System.Collections.Generic;

namespace Signawel.Dto.Reports
{
    public class ReportResponseDto
    {
        public string Id { get; set; }

        public DateTime CreationTime { get; set; }

        public string SenderEmail { get; set; }

        public string Description { get; set; }

        public string RoadworkId { get; set; }

        public ICollection<ReportImageResponseDto> Images { get; set; }

        public DefaultIssueResponseDto Issue { get; set; }

        public string Cities { get; set; }
    }
}
