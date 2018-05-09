using EvaluationFormsManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace EvaluationFormsManager.Persistence.EF
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; }
    }
}
