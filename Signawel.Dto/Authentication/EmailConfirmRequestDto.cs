using System.ComponentModel.DataAnnotations;

namespace Signawel.Dto.Authentication
{
    public class EmailConfirmRequestDto
    {
        /// <summary>
        ///     Id of the <see cref="User"/> to confirm the email address of.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        ///     Email confirmation token created by the UserManager
        /// </summary>
        [Required]
        public string Token { get; set; }
    }
}
