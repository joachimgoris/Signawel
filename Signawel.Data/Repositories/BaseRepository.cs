using Signawel.Data.Abstractions;
using Signawel.Domain;
using System.Threading.Tasks;

namespace Signawel.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly SignawelDbContext _context;

        public BaseRepository(SignawelDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddEntityAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteEntityAsync(string id)
        {
            var entity = await _context.FindAsync<T>(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetEntityAsync(string id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<T> ModifyEntityAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
