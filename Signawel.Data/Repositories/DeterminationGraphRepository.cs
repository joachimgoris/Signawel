using Microsoft.EntityFrameworkCore;
using Signawel.Data.Abstractions.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Signawel.Domain.Determination;

namespace Signawel.Data.Repositories
{
    public class DeterminationGraphRepository : IDeterminationRepository
    {
        private readonly SignawelDbContext _context;

        public DeterminationGraphRepository(SignawelDbContext context)
        {
            this._context = context;
        }

        public async Task<DeterminationGraph> GetGraphAsync()
        {
            var graph = await _context.DeterminationGraphs
                .Include(g => g.Start)
                .FirstOrDefaultAsync();
            
            if(graph != null)
                await LoadChilderen(graph.Start);

            return graph;
        }

        private async Task LoadChilderen(DeterminationNode node)
        {
            node.Answers = await _context.DeterminationAnswers
                .Include(a => a.Node)
                .Where(a => a.ParentNodeId == node.Id)
                .ToListAsync();

            foreach(var answer in node.Answers)
            {
                await LoadChilderen(answer.Node);
            }
        }

        public async Task<DeterminationGraph> SetGraphAsync(DeterminationGraph graph)
        {
            var currentGraph = await GetGraphAsync();
            if(currentGraph != null)
            {
                await DeleteChilderen(currentGraph.Start, true);
                _context.DeterminationGraphs.Remove(currentGraph);
                await _context.SaveChangesAsync();
            }

            _context.DeterminationGraphs.Add(graph);
            await _context.SaveChangesAsync();

            return graph;
        }

        private async Task DeleteChilderen(DeterminationNode node, bool deleteSelf = false)
        {
            if(node == null)
            {
                return;
            }

            foreach(var answer in node.Answers)
            {
                await DeleteChilderen(answer.Node);

                _context.DeterminationAnswers.Remove(answer);
                _context.DeterminationNodes.Remove(answer.Node);
            }

            if(deleteSelf)
            {
                _context.DeterminationNodes.Remove(node);
            }
        }
    }
}
