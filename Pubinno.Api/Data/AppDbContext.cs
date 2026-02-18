using Microsoft.EntityFrameworkCore;
using Pubinno.Api.Entities;

namespace Pubinno.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<PourEvent> Pours => Set<PourEvent>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PourEvent>()
                .HasKey(x => x.EventId);

            builder.Entity<PourEvent>()
                .HasIndex(x => new { x.DeviceId, x.StartedAt });
        }
    }

}
