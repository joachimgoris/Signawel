using System;
using Signawel.Dto.Determination;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IDeterminationService
    {

        Task<DeterminationGraphResponseDto> GetGraphAsync();

        Task<DeterminationGraphResponseDto> SetGraphAsync(DeterminationGraphCreationRequestDto creationDto);

    }
}
