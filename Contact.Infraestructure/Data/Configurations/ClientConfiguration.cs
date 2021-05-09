using Contact.Core.Enitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(c => c.ClientId);

            builder.Property(c => c.ClientId)
                .HasColumnName("ClienteId");                

            builder.Property(c => c.FullName)
                .HasColumnName("NombreCompleto")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.SocialNumber)
                .HasColumnName("Identificacion")
                .IsRequired()
                .HasMaxLength(100);            

        builder.Property(c => c.RecordDate)
                .HasColumnName("FechaRegistro")
                .HasColumnType("datetime");

        }
    }
}
