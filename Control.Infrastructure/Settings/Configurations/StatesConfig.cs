using Control.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Infrastructure.Settings.Configurations
{
    public class StatesConfig : IEntityTypeConfiguration<States>
    {
        public void Configure(EntityTypeBuilder<States> builder)
        {
            builder.ToTable("States");
            builder.HasKey(a => a.IdState);

            builder.Property(a => a.IdState).HasColumnName("idState")
                   .HasColumnType<int>("Int")
                   .HasComment("Identificador del pais")
                   .IsRequired(); 
          

            builder.Property(a => a.NameState).HasColumnName("nameState")
                   .HasColumnType<string>("nvarchar(50)")
                   .HasComment("Nombre del pais")
                   .HasMaxLength(50);

            builder.Property(a => a.IdCountry).HasColumnName("idCountry")
                   .HasColumnType<int>("Int")
                   .HasComment("Identificador del pais")
                   .IsRequired();



            builder.Property(a => a.Status).HasColumnName("status")
                   .HasColumnType<bool>("bit")
                   .HasComment("Estado del pais")
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.HasOne(a => a.Countries)
                .WithMany(c => c.States)
                .HasForeignKey(a => a.IdCountry)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
