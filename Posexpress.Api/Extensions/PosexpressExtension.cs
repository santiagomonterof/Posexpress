using Microsoft.EntityFrameworkCore;
using Posexpress.Infrastructure.Persistence.EntityFramework;
using Posexpress.Core.Common.Policies;
using Posexpress.Core.Common.Services;
using Posexpress.Core.Modules.ERP.Barcodes.Interfaces;
using Posexpress.Core.Modules.ERP.ErpProducts.Interfaces;
using Posexpress.Core.Modules.Express.ExpSales.Interfaces;
using Posexpress.Core.Modules.Express.ProductCategories.Interfaces;
using Posexpress.Infrastructure.Modules.ERP.Barcodes;
using Posexpress.Infrastructure.Modules.ERP.ErpProducts;
using Posexpress.Infrastructure.Modules.Express.ExpSales;
using Posexpress.Infrastructure.Modules.Express.ProductCategories;

namespace Posexpress.Api.Extensions;

public static class PosexpressExtension
{
    public static IServiceCollection AddPosexpressServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CustomDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("Default")));

        services.AddSingleton<IUniqueCodeGenerator, SimpleUniqueCodeGenerator>();

        // Si cambias "BusinessStrategy" en appsettings.json a "GanaMax", automáticamente se aplican las reglas GanaMax.
        // Si lo dejas en "Base", se mantienen las reglas base.
        var strategy = config.GetValue<string>("BusinessStrategy");
        if (string.Equals(strategy, "GanaMax", StringComparison.OrdinalIgnoreCase))
            services.AddScoped<IBusinessStrategy, GanaMaxBusinessStrategy>();
        else
            services.AddScoped<IBusinessStrategy, BaseBusinessStrategy>();

        services.AddScoped<IBarcodeProvider, BarcodeProvider>();
        services.AddScoped<IErpProductProvider, ErpProductProvider>();
        services.AddScoped<IExpSaleProvider, ExpSaleProvider>();
        services.AddScoped<IProductCategoryProvider, ProductCategoryProvider>();

        return services;
    }
}
