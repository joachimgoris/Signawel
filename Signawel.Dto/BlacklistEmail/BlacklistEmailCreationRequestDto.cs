using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.BlacklistEmail
{
    public class BlacklistEmailCreationRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
