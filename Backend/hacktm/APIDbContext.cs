using hacktm.Entities;
using Microsoft.EntityFrameworkCore;

namespace hacktm
{
    public class APIDbContext : DbContext
    {
        public DbSet<Street> Streets { get; set; }
        public APIDbContext()
        {

        }

        public APIDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Street>();
        }
    }
}
