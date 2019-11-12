using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Signawel.Data.Configurations;
using Signawel.Domain;

namespace Signawel.Data
{
    public class SignawelDbContext : IdentityDbContext<User, Role, string>
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
            builder.ApplyConfiguration(new ReportImageConfiguration());
            builder.ApplyConfiguration(new LoginRecordsConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new DeterminationGraphConfiguration());
            builder.ApplyConfiguration(new DeterminationNodeConfiguration());
            builder.ApplyConfiguration(new DeterminationAnswerConfiguration());
        }

        #endregion

        #region DbSets

        public DbSet<DefaultIssue> DefaultIssues { get; set; }
        public DbSet<PriorityEmail> PriorityEmails { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }
        public DbSet<ReportIssue> ReportIssues { get; set; }
        public DbSet<DeterminationGraph> DeterminationGraphs { get; set; }
        public DbSet<DeterminationNode> DeterminationNodes { get; set; }
        public DbSet<DeterminationAnswer> DeterminationAnswers { get; set; }

        // Authentiction
        public DbSet<LoginRecord> LoginRecords { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion
    }
}
 