using Signawel.Domain;
using Signawel.Dto.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IJwtTokenFactory
    {
        Task<TokenResponseDto> GenerateToken(User user, ICollection<Claim> additionalClaims);

        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
