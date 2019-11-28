
using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.ReportGroup
{
    public class CityCreationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
