using Microsoft.EntityFrameworkCore;

namespace EvaluationFormsManager.Persistence.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<EvaluationFormsManager.Domain.Form> Forms { get; set; }
    }
}
