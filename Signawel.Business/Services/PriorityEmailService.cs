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
        private IQueryable<PriorityEmail> _priorityEmails;

        public PriorityEmailService(SignawelDbContext context)
        {
            _context = context;
            _priorityEmails = _context.PriorityEmails;
        }

        public async Task<bool> CheckPriorityEmailAsync(string emailSuffix)
        {
            foreach (PriorityEmail mail in _priorityEmails)
            {
                if (emailSuffix.Equals(mail.EmailSuffix))
                {
                    return true;
                }
            }
            return false;
        }

        public IQueryable<PriorityEmail> GetPriorityEmails()
        {
           return _context.PriorityEmails;
        }
    }
}
