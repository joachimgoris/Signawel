using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class PriorityEmailService : IPriorityEmailService
    {
        private readonly SignawelDbContext _context;

        public PriorityEmailService(SignawelDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckPriorityEmailAsync(string emailSuffix)
        {
            return await _context.PriorityEmails.AnyAsync(pe => pe.EmailSuffix.Equals(emailSuffix));
        }

        public IQueryable<PriorityEmail> GetPriorityEmails()
        {
           return _context.PriorityEmails;
        }
    }
}
