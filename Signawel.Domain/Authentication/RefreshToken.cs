using System;
using System.ComponentModel.DataAnnotations;

namespace Signawel.Domain
{
    public class RefreshToken : Entity
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string JwtId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public bool Used { get; set; }

        public bool Invalidated { get; set; }
        
        public User User { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
