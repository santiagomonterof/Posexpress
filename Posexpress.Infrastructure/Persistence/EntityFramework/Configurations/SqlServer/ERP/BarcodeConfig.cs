using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posexpress.Core.Modules.ERP.Barcodes.Entities;

namespace Posexpress.Infrastructure.Persistence.EntityFramework.Configurations.SqlServer.ERP;

public class BarcodeConfig : IEntityTypeConfiguration<Barcode>
{
    public void Configure(EntityTypeBuilder<Barcode> builder)
    {
        builder.ToTable("CodigosBarras");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("IdCodigoBarra")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UniqueCode)
            .IsRequired()
            .HasMaxLength(64)
            .HasColumnName("UniqueCodigo");

        builder.HasIndex(p => p.UniqueCode)
               .IsUnique();

        builder.Property(p => p.Active)
            .HasColumnName("Activo")
            .HasDefaultValue(true);

        builder.Property(p => p.ProductId)
            .HasColumnName("IdProducto");

        builder.HasOne(p => p.Product)
               .WithMany(p => p.Barcodes)
               .HasForeignKey(p => p.ProductId);
    }
}
