using Microsoft.EntityFrameworkCore;
using EvaluationFormsManager.Domain;
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

        public DbSet<Form> Forms { get; set; }
        public DbSet<Importance> Importances { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
    }
}
