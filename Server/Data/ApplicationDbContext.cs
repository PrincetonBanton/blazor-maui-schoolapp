// File: SchoolApp.Server/Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using SchoolApp.Shared.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchoolApp.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Subcomponent> Subcomponents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SubGradingItem> SubGradingItems { get; set; }
        public DbSet<SubGradingScore> SubGradingScores { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<GradeLevel> GradeLevels { get; set; }


    }
}
