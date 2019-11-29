using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.PriorityEmail
{
    public class PriorityEmailCreationRequestDto
    {

        [Required]
        public string EmailSuffix { get; set; }

    }
}
