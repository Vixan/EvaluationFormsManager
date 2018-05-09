﻿using EvaluationFormsManager.Domain;
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
    }
}
