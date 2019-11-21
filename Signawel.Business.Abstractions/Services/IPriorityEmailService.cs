using Signawel.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IPriorityEmailService
    {
        Task<bool> CheckPriorityEmailAsync(string emailSuffix);
        IQueryable<PriorityEmail> GetPriorityEmails();
    }
}
