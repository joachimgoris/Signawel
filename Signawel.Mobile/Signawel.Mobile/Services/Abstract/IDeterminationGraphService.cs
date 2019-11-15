using System.Threading.Tasks;
using Signawel.Dto.Determination;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IDeterminationGraphService
    {

        Task<DeterminationGraphResponseDto> GetDeterminationGraph();

    }
}