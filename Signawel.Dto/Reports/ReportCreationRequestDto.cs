using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Signawel.Domain.Reports;

namespace Signawel.Dto.Reports
{
    public class ReportCreationRequestDto
    {
        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        public string Description { get; set; }

        [Required]
        public string RoadworkId { get; set; }

        public ICollection<ReportImage> Images { get; set; }

        [Required]
        public string IssueId { get; set; }

        public string Cities { get; set; }

        public DateTime CreationTime { get; set; }
    }
}