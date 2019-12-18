using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.ReportGroup
{
    public class ReportGroupResponseDto
    {
        public string Id { get; set; }

        [Required]
        public List<CityResponseDto> CityReportGroups { get; set; }

        [Required]
        public List<EmailResponseDto> EmailReportGroups { get; set; }

        [Required]
        public List<UserResponseDto> UserReportGroups { get; set; }

    }
}
