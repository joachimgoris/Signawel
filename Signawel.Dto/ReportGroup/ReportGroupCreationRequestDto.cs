using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.ReportGroup
{
    public class ReportGroupCreationRequestDto
    {
        [Required]
        public List<CityCreationRequestDto> CityReportGroups { get; set; }

        [Required]
        public List<EmailCreationRequestDto> EmailReportGroups { get; set; }

        [Required]
        public List<UserCreationRequestDto> UserReportGroups { get; set; }

    }
}
