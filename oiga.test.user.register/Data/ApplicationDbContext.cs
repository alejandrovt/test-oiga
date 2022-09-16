using Microsoft.EntityFrameworkCore;
using oiga.test.user.common;

namespace oiga.test.user.register.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> EntityUsers { get; set; }

        public void RefreshAll()
        {
            foreach (var entity in this.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }
    }
}
