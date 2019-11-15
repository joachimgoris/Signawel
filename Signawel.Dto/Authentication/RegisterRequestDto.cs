using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.Authentication
{
    public class RegisterRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must be at least 8 characters long", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string PasswordRepeat { get; set; }
    }
}
