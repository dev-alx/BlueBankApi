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
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("Cuenta");

            builder.HasKey(c => c.BankAccountId);

            builder.Property(c => c.BankAccountId)
                .HasColumnName("CuentaId");

            builder.Property(c => c.Description)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Product)
                .HasColumnName("Producto")
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(c => c.InitialBalance)
                .HasColumnName("SaldoInicial")
                .IsRequired()
                .HasColumnType("decimal(10,4)");                       

            builder.Property(c => c.RecordDate)
                .HasColumnName("FechaRegistro")
                .HasColumnType("datetime");
        }
    }
}
