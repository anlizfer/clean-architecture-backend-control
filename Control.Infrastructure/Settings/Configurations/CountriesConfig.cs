using Control.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.Infrastructure.Settings.Configurations
{
    public class CountriesConfig : IEntityTypeConfiguration<Countries>
    {
        public void Configure(EntityTypeBuilder<Countries> builder)
        {

            builder.ToTable("Countries");
            builder.HasKey(a => a.IdCountry);

            builder.Property(a => a.IdCountry)
                   .HasColumnName("idCountry")
                   .HasColumnType<int>("Int")
                   .HasComment("Identificador del pais")
                   .IsRequired();

            builder.Property(c => c.NameCountry)
                   .HasColumnName("nameCountry")
                   .HasColumnType<string>("nvarchar(50)")
                   .HasComment("Nombre del pais")
                   .HasMaxLength(50)
                   .IsRequired();

            

            builder.Property(c => c.Status)
                   .HasColumnName("status")
                   .HasColumnType<bool>("bit")
                   .HasComment("Estado del pais")
                   .HasDefaultValue(false)
                   .IsRequired();



        }
    }
}
