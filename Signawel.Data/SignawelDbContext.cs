using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Signawel.Data.Configurations;
using Signawel.Domain;
using Signawel.Domain.BBox;
using Signawel.Domain.Determination;
using Signawel.Domain.ReportGroups;
using Signawel.Domain.Reports;

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
            builder.ApplyConfiguration(new ReportDefaultIssueConfiguration());
            builder.ApplyConfiguration(new PriorityEmailConfiguration());
            builder.ApplyConfiguration(new BlacklistEmailConfiguration());
            builder.ApplyConfiguration(new ReportImageConfiguration());
            builder.ApplyConfiguration(new LoginRecordsConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new DeterminationGraphConfiguration());
            builder.ApplyConfiguration(new DeterminationNodeConfiguration());
            builder.ApplyConfiguration(new DeterminationAnswerConfiguration());
            builder.ApplyConfiguration(new BBoxConfiguration());
            builder.ApplyConfiguration(new BBoxPointConfiguration());
            builder.ApplyConfiguration(new RoadworkSchemaConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new EmailConfiguration());
            builder.ApplyConfiguration(new ReportGroupConfiguration());
            builder.ApplyConfiguration(new CityReportGroupConfiguration());
            builder.ApplyConfiguration(new EmailReportGroupConfiguration());
            builder.ApplyConfiguration(new UserReportGroupConfiguration());
        }

        #endregion

        #region DbSets

        public DbSet<ReportDefaultIssue> DefaultIssues { get; set; }
        public DbSet<PriorityEmail> PriorityEmails { get; set; }
        public DbSet<BlacklistEmail> BlacklistEmails { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }
        public DbSet<DeterminationGraph> DeterminationGraphs { get; set; }
        public DbSet<DeterminationNode> DeterminationNodes { get; set; }
        public DbSet<DeterminationAnswer> DeterminationAnswers { get; set; }
        public DbSet<LoginRecord> LoginRecords { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<BBox> BoundingBox { get; set; }
        public DbSet<BBoxPoint> BoundingBoxPoint { get; set; }
        public DbSet<RoadworkSchema> RoadworkSchemas { get; set; }
        public DbSet<Image> Images { get; set;}
        public DbSet<ReportGroup> ReportGroups { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<CityReportGroup> CityReportGroups { get; set; }
        public DbSet<EmailReportGroup> EmailReportGroups { get; set; }
        public DbSet<UserReportGroup> UserReportGroups { get; set; }

        #endregion
    }
}
 