using EvaluationFormsManager.Domain;
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

        public DbSet<EvaluationFormsManager.Domain.Form> Forms { get; set; }
        public DbSet<EvaluationFormsManager.Domain.Importance> Importances { get; set; }
        public DbSet<EvaluationFormsManager.Domain.Status> Statuses { get; set; }
        public DbSet<EvaluationFormsManager.Domain.Section> Sections { get; set; }
        public DbSet<EvaluationFormsManager.Domain.Criteria> Criteria { get; set; }
        public DbSet<EvaluationFormsManager.Domain.EvaluationScale> EvaluationScales { get; set; }
        public DbSet<EvaluationFormsManager.Domain.EvaluationScaleOption> EvaluationScaleOptions { get; set; }
    }
}
