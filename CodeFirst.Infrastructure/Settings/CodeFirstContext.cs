using CodeFirst.Domain.Entities;
using CodeFirst.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeFirst.Infrastructure.Settings
{
    //public class CodeFirstContext : DbContext
    public class CodeFirstContext : IdentityDbContext
    {
        public CodeFirstContext()
        {
        }

        public CodeFirstContext(DbContextOptions<CodeFirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Alumnos { get; set; }
        public virtual DbSet<Course> Cursos { get; set; }
        public virtual DbSet<Inscription> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.SeedData();
        }
    }
}