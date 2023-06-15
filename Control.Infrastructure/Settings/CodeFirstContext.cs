using Control.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Control.Infrastructure.Settings
{
    //public class ControlContext : DbContext
    public class ControlContext : IdentityDbContext
    {
        public ControlContext()
        {
        }

        public ControlContext(DbContextOptions<ControlContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<DocumentType> TipoDocumento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}