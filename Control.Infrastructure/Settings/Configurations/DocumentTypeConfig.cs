using Control.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Control.Infrastructure.Settings.Configurations
{
    public class DocumentTypeConfig : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {

            builder.ToTable("DocumentType");
            builder.HasKey(a => a.IdDocumentType);

            builder.Property(a => a.IdDocumentType)
                   .HasColumnName("IdDocumentType")
                   .HasColumnType<long>("BIGINT")
                   .HasComment("Identificador del tipo de documento")
                   .IsRequired();

            builder.Property(c => c.NameDocumentType)
                   .HasColumnName("NameDocumentType")
                   .HasColumnType<string>("nvarchar(50)")
                   .HasComment("Nombre del tipo de documento")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(c => c.Status)
                   .HasColumnName("Status")
                   .HasColumnType<bool>("bit")
                   .HasComment("Estado del tipo documento")
                   .HasDefaultValue(false)
                   .IsRequired();



        }
    }
}
