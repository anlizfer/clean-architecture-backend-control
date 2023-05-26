using CodeFirst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CodeFirst.Infrastructure.Settings.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student", "University");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .HasColumnName("StudentId")
                   .HasColumnType<long>("BIGINT")
                   .HasComment("Identificador del Alumno")
                   .IsRequired();

            builder.Property(a => a.Name)
                   .HasColumnName("Name")
                   .HasColumnType<string>("nvarchar(50)")
                   .HasComment("Nombre del Alumno")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(a => a.DateOfBirth)
                   .HasColumnName("DateOfBirth")
                   .HasColumnType<DateTime>("date")
                   .HasComment("Fecha de nacimiento del Alumno")
                   .IsRequired();
        }
    }
}