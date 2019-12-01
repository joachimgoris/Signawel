using System;
using System.Collections.Generic;
using Signawel.Domain.Reports;

namespace Signawel.Dto.Reports
{
    public class ReportGetResponseDto
    {
        public string Id { get; set; }

        public DateTime CreationTime { get; set; }

        public string SenderEmail { get; set; }

        public string Description { get; set; }

        public string RoadworkId { get; set; }

        public ICollection<ReportImage> Images { get; set; }

        public ReportDefaultIssue Issue { get; set; }
    }
}
