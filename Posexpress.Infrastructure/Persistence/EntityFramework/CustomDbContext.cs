using Microsoft.EntityFrameworkCore;
using Posexpress.Core.Modules.Express.ExpProducts.Entities;
using Posexpress.Core.Modules.Express.ExpSales.Entities;
using Posexpress.Core.Modules.Express.Categories.Entities;
using Posexpress.Core.Modules.Express.ProductCategories.Entities;
using Posexpress.Core.Modules.Express.ProductTypes.Entities;
using Posexpress.Core.Modules.ERP.ErpProducts.Entities;
using Posexpress.Core.Modules.ERP.Barcodes.Entities;

namespace Posexpress.Infrastructure.Persistence.EntityFramework;

public class CustomDbContext(DbContextOptions<CustomDbContext> options) : DbContext(options)
{
    public DbSet<ExpProduct> ExpProducts => Set<ExpProduct>();
    public DbSet<SaleExp> ExpSales => Set<SaleExp>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ProductCategory> ProductsCategories => Set<ProductCategory>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<ErpProduct> ErpProducts => Set<ErpProduct>();
    public DbSet<Barcode> Barcodes => Set<Barcode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CustomDbContext).Assembly,
            t => t.Namespace != null &&
                 t.Namespace.StartsWith("Posexpress.Infrastructure.Persistence.EntityFramework.Configurations.SqlServer"));

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CustomDbContext).Assembly,
            t => t.Namespace != null &&
                 t.Namespace.StartsWith("Posexpress.Infrastructure.Persistence.EntityFramework.Seeders.SqlServer"));

        base.OnModelCreating(modelBuilder);
    }
}
