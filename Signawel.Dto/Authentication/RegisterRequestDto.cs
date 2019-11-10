using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.Authentication
{
    public class RegisterRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordRepeat { get; set; }
    }
}
