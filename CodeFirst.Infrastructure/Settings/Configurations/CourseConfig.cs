using CodeFirst.Domain.Entities;
using CodeFirst.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CodeFirst.Infrastructure.Settings.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course", "University");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("CourseId")
                   .HasColumnType<long>("BIGINT")
                   .HasComment("Identificador del curso")
                   .IsRequired();

            builder.Property(c => c.Name)
                   .HasColumnName("Name")
                   .HasColumnType<string>("nvarchar(50)")
                   .HasComment("Nombre del curso")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(c => c.StateId)
                   .HasColumnName("State")
                   .HasColumnType<Estado>("nvarchar(50)")
                   .HasComment("Estado del curso")
                   .HasMaxLength(15)
                   .HasConversion(
                                   x => x.ToString(),
                                   x => (Estado)Enum.Parse(typeof(Estado), x)
                                 )
                   .IsRequired();

            builder.Property(c => c.IsDeleted)
                   .HasColumnName("IsDeleted")
                   .HasColumnType<bool>("bit")
                   .HasComment("Nombre del curso")
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(c => c.CreatedBy)
                   .HasColumnName("CreatedBy")
                   .HasColumnType<string>("nvarchar(50)")
                   .HasComment("Usuario que creo el registro")
                   .HasDefaultValue("Angel Fernando Lizcano Novoa")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(a => a.CreatedDate)
                   .HasColumnName("CreatedDate")
                   .HasColumnType<DateTime>("datetime")
                   .HasComment("Fecha que se creo el registro")
                   .HasDefaultValue("2022 - 03 - 01 07:00:0.0")
                   .IsRequired();

            builder.Property(c => c.UpdatedBy)
                   .HasColumnName("UpdatedBy")
                   .HasColumnType<string>("nvarchar(50)")
                   .HasComment("Usuario que actualizo el registro")
                   .HasDefaultValue("")
                   .HasMaxLength(50);

            builder.Property(a => a.UpdatedDate)
                   .HasColumnName("UpdatedDate")
                   .HasColumnType<DateTime?>("datetime")
                   .HasComment("Fecha que se actualizo el registro")
                   .HasDefaultValue(null);
        }
    }
}