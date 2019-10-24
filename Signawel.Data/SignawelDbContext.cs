using Microsoft.EntityFrameworkCore;

namespace Signawel.Data
{
    public class SignawelDbContext : DbContext
    {
        public SignawelDbContext(DbContextOptions options) : base(options) { }
    }
}
