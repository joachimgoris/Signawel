using System.Threading.Tasks;
using Signawel.Domain.Determination;

namespace Signawel.Data.Abstractions.Repositories
{
    public interface IDeterminationRepository
    {

        Task<DeterminationGraph> GetGraphAsync();

        Task<DeterminationGraph> SetGraphAsync(DeterminationGraph graph);

    }
}
