using Microsoft.EntityFrameworkCore;
using Signawel.Data.Configurations;
using Signawel.Domain;

namespace Signawel.Data
{
    public class SignawelDbContext : DbContext
    {
        public SignawelDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplyConfigurations(modelBuilder);
        }

        #region Private Methods

        private void ApplyConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ReportConfiguration());
            builder.ApplyConfiguration(new ReportIssueConfiguration());
            builder.ApplyConfiguration(new DefaultIssueConfiguration());
            builder.ApplyConfiguration(new PriorityEmailConfiguration());
            builder.ApplyConfiguration(new ReportIssueConfiguration());
        }

        #endregion

        #region DbSets

        public DbSet<DefaultIssue> DefaultIssues { get; set; }
        public DbSet<PriorityEmail> PriorityEmails { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }
        public DbSet<ReportIssue> ReportIssues { get; set; }

        #endregion
    }
}
