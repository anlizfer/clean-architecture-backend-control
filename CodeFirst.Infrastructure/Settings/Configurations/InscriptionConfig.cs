using CodeFirst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst.Infrastructure.Settings.Configurations
{
    public class InscriptionConfig : IEntityTypeConfiguration<Inscription>
    {
        public void Configure(EntityTypeBuilder<Inscription> builder)
        {
            builder.ToTable("Inscription", "University");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                   .HasColumnName("InscriptionId")
                   .HasColumnType<long>("BIGINT")
                   .HasComment("Identificador la inscripcion")
                   .IsRequired();

            builder.Property(i => i.StudentId)
                   .HasColumnName("StudentId")
                   .HasColumnType<long>("BIGINT")
                   .HasComment("Identificador del alumno")
                   .IsRequired();

            builder.Property(i => i.CourseId)
                   .HasColumnName("CourseId")
                   .HasColumnType<long>("BIGINT")
                   .HasComment("Identificador del curso")
                   .IsRequired();

            builder.HasOne(d => d.Students)
                   .WithMany(p => p.Inscriptions)
                   .HasForeignKey(d => d.StudentId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Inscriptions_Students");

            builder.HasOne(d => d.Courses)
                   .WithMany(p => p.Inscriptions)
                   .HasForeignKey(d => d.CourseId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Inscriptions_Courses");
        }
    }
}