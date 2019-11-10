using System;

namespace Signawel.Domain.Authentication
{
    /// <summary>
    ///     Configuration class for tokens.
    /// </summary>
    public class TokenConfiguration
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan TokenLifetime { get; set; }

        public int RefreshTokenLifetime { get; set; }

    }
}
