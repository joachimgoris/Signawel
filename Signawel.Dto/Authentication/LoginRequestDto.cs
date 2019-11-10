using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.Authentication
{
    public class LoginRequestDto
    {
        /// <summary>
        ///     Email address of the <see cref="User"/>.
        /// </summary>
        [EmailAddress, Required]
        public string Email { get; set; }

        /// <summary>
        ///     Password of the user <see cref="User"/>.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
