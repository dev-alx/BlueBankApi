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
    public class AccountMovementConfiguration : IEntityTypeConfiguration<AccountMovement>
    {
        public void Configure(EntityTypeBuilder<AccountMovement> builder)
        {
            builder.ToTable("Movimiento");

            builder.HasKey(c => c.AccountMovementId);

            builder.Property(c => c.AccountMovementId)
                .HasColumnName("MovimientoId");

            builder.Property(c => c.MovementType)
                .HasColumnName("TipoMovimiento")
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Amount)
                .HasColumnName("Cantidad")
                .IsRequired()
                .HasColumnType("decimal(10,4)");

            builder.Property(c => c.RecorDate)
                .HasColumnName("FechaRegistro")
                .HasColumnType("datetime");
        }
    }
}
