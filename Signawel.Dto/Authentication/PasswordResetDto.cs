namespace Signawel.Dto.Authentication
{
    public class PasswordResetDto
    {
        public string Email { get; set; }

        public string ResetToken { get; set; }

        public string NewPassword { get; set; }
    }
}
