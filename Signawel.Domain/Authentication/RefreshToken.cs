using System;

namespace Signawel.Domain
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime ExpiryDate { get; set; }
        
        public User User { get; set; }

        public string UserId { get; set; }
    }
}
