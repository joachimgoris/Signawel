namespace Signawel.Dto.Authentication
{
    public class RefreshRequestDto
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
