﻿// File: SchoolApp.Server/Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using SchoolApp.Shared.Models;
using System.Collections.Generic;

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
        public DbSet<GradeEntry> GradeEntries { get; set; }
    }
}
