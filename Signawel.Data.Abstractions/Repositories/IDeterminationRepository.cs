using Signawel.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Data.Abstractions.Repositories
{
    public interface IDeterminationRepository
    {

        Task<DeterminationGraph> GetGraphAsync();

        Task<DeterminationGraph> SetGraphAsync(DeterminationGraph graph);

    }
}
