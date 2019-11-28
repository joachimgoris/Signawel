using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.ReportGroup
{
    public class EmailCreationRequestDto
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
