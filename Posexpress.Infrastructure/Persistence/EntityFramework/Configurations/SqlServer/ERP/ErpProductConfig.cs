using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Posexpress.Core.Modules.ERP.ErpProducts.Entities;

namespace Posexpress.Infrastructure.Persistence.EntityFramework.Configurations.SqlServer.ERP;

public class ErpProductConfig : IEntityTypeConfiguration<ErpProduct>
{
    public void Configure(EntityTypeBuilder<ErpProduct> builder)
    {
        builder.ToTable("ErpProductos");

        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.ProductId)
            .HasColumnName("IdProducto")
            .ValueGeneratedNever();

        builder.Property(p => p.Cost)
            .HasColumnType("decimal(18,2)")
            .HasColumnName("Costo");

        builder.Property(p => p.UniqueCode)
            .IsRequired()
            .HasMaxLength(64)
            .HasColumnName("UniqueCodigo");

        builder.HasIndex(p => p.UniqueCode)
               .IsUnique();

        builder.Property(p => p.RegisteredAt)
            .HasColumnName("FechaRegistro");

        builder.Property(p => p.Stock)
            .HasColumnName("Stock");

        builder.HasOne(p => p.Product)
               .WithOne() 
               .HasForeignKey<ErpProduct>(p => p.ProductId);
    }
}
