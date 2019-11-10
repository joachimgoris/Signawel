using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;

namespace Signawel.API.Extensions
{
    /// <summary>
    ///     Extension methods on the <see cref="HttpContext"/> class.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Get remote ip address, optionally allowing for x-forwarded-for header check
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="allowForwarded">Whether to allow x-forwarded-for header check</param>
        /// <returns>IPAddress</returns>
        public static IPAddress GetRemoteIpAddress(this HttpContext context, bool allowForwarded = true)
        {
            if (!allowForwarded)
            {
                return context.Connection.RemoteIpAddress;
            }

            var header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());

            if (header != null && IPAddress.TryParse(header, out var ip))
            {
                return ip;
            }

            return context.Connection.RemoteIpAddress;
        }
    }
}
