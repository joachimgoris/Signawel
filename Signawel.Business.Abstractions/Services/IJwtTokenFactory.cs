using Signawel.Domain;
using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IJwtTokenFactory
    {
        Task<DataResult<TokenResponseDto>> GenerateToken(User user, ICollection<Claim> additionalClaims);

        DataResult<ClaimsPrincipal> GetPrincipalFromToken(string token);
    }
}
