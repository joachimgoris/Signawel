using Microsoft.EntityFrameworkCore;

namespace Signawel.MobileData
{
    public class SignawelMobileContext : DbContext
    {

        public DbSet<DbToken> DbToken { get; set; }

        public SignawelMobileContext(DbContextOptions<SignawelMobileContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbToken>(entity =>
            {
                entity.ToTable("auth_token");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .IsRequired();

                entity.Property(e => e.RefreshToken)
                    .HasColumnName("refresh_token")
                    .IsRequired();
            });
        }
    }
}
