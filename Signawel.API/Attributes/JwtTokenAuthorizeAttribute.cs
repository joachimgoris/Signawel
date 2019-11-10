using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Signawel.API.Attributes
{
    public class JwtTokenAuthorizeAttribute : AuthorizeAttribute
    {
        public JwtTokenAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
