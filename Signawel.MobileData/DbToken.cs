using System;
using System.ComponentModel.DataAnnotations;

namespace Signawel.MobileData
{
    public class DbToken
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
