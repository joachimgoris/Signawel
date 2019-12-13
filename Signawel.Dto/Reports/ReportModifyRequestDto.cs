using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Signawel.Domain.Reports;

namespace Signawel.Dto.Reports
{
    public class ReportModifyRequestDto
    {
        public string Id { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public string Description { get; set; }

        public string RoadworkId { get; set; }

        public ICollection<ReportImage> Images { get; set; }

        public int DefaultIssueId { get; set; }

        public string Cities { get; set; }
    }
}
