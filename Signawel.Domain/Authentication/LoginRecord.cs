using System;

namespace Signawel.Domain
{
    public class LoginRecord : Entity
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public DateTime Time { get; } = DateTime.Now;

        public string IpAddress { get; set; }

        public bool Succes { get; set; }
    }
}
