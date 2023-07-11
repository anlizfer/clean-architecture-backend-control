using Control.Domain.Entities;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Control.Infrastructure.Settings
{
    //public class ControlContext : DbContext
    public class ControlContext : DbContext
    {
        public ControlContext()
        {
        }

        public ControlContext(DbContextOptions<ControlContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<DocumentType> TipoDocumento { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<States> States {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}