using Corenexus.Core.Bases.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Posexpress.Core.Common.Policies;
using Posexpress.Core.Common.Services;
using Posexpress.Core.Modules.ERP.ErpProducts.Dtos.Commands;
using Posexpress.Core.Modules.ERP.ErpProducts.Entities;
using Posexpress.Core.Modules.ERP.ErpProducts.Interfaces;
using Posexpress.Core.Modules.Express.ExpProducts.Entities;
using Posexpress.Core.Modules.Express.ProductTypes.Entities;
using Posexpress.Infrastructure.Persistence.EntityFramework;

namespace Posexpress.Infrastructure.Modules.ERP.ErpProducts;

public class ErpProductProvider(
    CustomDbContext context,
    ILogger<ErpProductProvider> logger,
    IUniqueCodeGenerator codeGen,
    IBusinessStrategy strategy
) : IErpProductProvider
{
    private readonly CustomDbContext _context = context;
    private readonly ILogger<ErpProductProvider> _logger = logger;
    private readonly IUniqueCodeGenerator _codeGen = codeGen;
    private readonly IBusinessStrategy _strategy = strategy;

    public async Task<Result<ErpProduct>> SaveErpProductAsync(CreateErpProductCommand command, CancellationToken ct)
    {
        if (command is null)
            return Result.Fail<ErpProduct>("Invalid request.", "erp_invalid_command");

        if (string.IsNullOrWhiteSpace(command.Name))
            return Result.Fail<ErpProduct>("Product name is required.", "erp_name_required");

        if (command.ProductTypeId <= 0)
            return Result.Fail<ErpProduct>("ProductTypeId is invalid.", "erp_type_required");

        if (command.Cost < 0)
            return Result.Fail<ErpProduct>("Cost must be greater or equal than zero.", "erp_cost_invalid");

        var typeExists = await _context.Set<ProductType>()
            .AnyAsync(t => t.Id == command.ProductTypeId, ct);

        if (!typeExists)
            return Result.Fail<ErpProduct>("Product type not found.", "product_type_not_found");

        string uniqueCode;
        const int maxAttempts = 5;
        var attempts = 0;

        do
        {
            uniqueCode = _codeGen.NewProductCode();
            attempts++;
            var exists = await _context.Set<ErpProduct>()
                .AnyAsync(e => e.UniqueCode == uniqueCode, ct);

            if (!exists) break;

            _logger.LogWarning("Collision generating ERP product code: {Code} (attempt {Attempt})", uniqueCode, attempts);
        } while (attempts < maxAttempts);

        if (attempts >= maxAttempts)
            return Result.Fail<ErpProduct>("Unable to generate a unique ERP product code.", "erp_unique_generation_failed");

        using var tx = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            var price = _strategy.CalculatePrice(command.Cost); 

            var exp = new ExpProduct
            {
                Name = command.Name.Trim(),
                Price = price,
                Active = true,
                ExpirationDate = null,
                Observations = command.Observations,
                ProductTypeId = command.ProductTypeId
            };

            await _context.Set<ExpProduct>().AddAsync(exp, ct);
            await _context.SaveChangesAsync(ct); 

            var erp = new ErpProduct
            {
                ProductId = exp.Id,           
                UniqueCode = uniqueCode,
                Cost = command.Cost,
                Stock = command.InitialStock,
                RegisteredAt = DateTime.UtcNow
            };

            await _context.Set<ErpProduct>().AddAsync(erp, ct);
            await _context.SaveChangesAsync(ct);

            await tx.CommitAsync(ct);

            await _context.Entry(erp).Reference(e => e.Product).LoadAsync(ct);

            return Result.Success(erp);
        }
        catch (DbUpdateException ex)
        {
            await tx.RollbackAsync(ct);
            _logger.LogError(ex, "Error saving ERP product.");
            return Result.Fail<ErpProduct>("Error saving ERP product.", "erp_persistence_error");
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync(ct);
            _logger.LogError(ex, "Unexpected error saving ERP product.");
            return Result.Fail<ErpProduct>("Unexpected error.", "erp_unexpected_error");
        }
    }
}
