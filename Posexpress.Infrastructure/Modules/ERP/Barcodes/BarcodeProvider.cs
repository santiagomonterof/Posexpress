using Corenexus.Core.Bases.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Posexpress.Core.Common.Services;
using Posexpress.Core.Modules.ERP.Barcodes.Dtos.Commands;
using Posexpress.Core.Modules.ERP.Barcodes.Entities;
using Posexpress.Core.Modules.ERP.Barcodes.Interfaces;
using Posexpress.Core.Modules.ERP.ErpProducts.Entities;
using Posexpress.Infrastructure.Persistence.EntityFramework;

namespace Posexpress.Infrastructure.Modules.ERP.Barcodes;

public class BarcodeProvider(
    CustomDbContext context,
    ILogger<BarcodeProvider> logger,
    IUniqueCodeGenerator codeGen
) : IBarcodeProvider
{
    private readonly CustomDbContext _context = context;
    private readonly ILogger<BarcodeProvider> _logger = logger;
    private readonly IUniqueCodeGenerator _codeGen = codeGen;

    public async Task<Result<Barcode>> AssignBarcodeAsync(AssignBarcodeCommand command, CancellationToken ct)
    {
        if (command is null)
            return Result.Fail<Barcode>("Invalid request.", "barcode_invalid_command");

        var erp = await _context.Set<ErpProduct>()
            .FirstOrDefaultAsync(x => x.ProductId == command.ErpProductId, ct);

        if (erp is null)
            return Result.Fail<Barcode>("ERP product not found.", "erp_product_not_found");

        string uniqueCode;
        const int maxAttempts = 5;
        var attempts = 0;

        do
        {
            uniqueCode = _codeGen.NewBarcodeCode();
            attempts++;
            var exists = await _context.Set<Barcode>()
                .AnyAsync(b => b.UniqueCode == uniqueCode, ct);

            if (!exists) break;

            _logger.LogWarning("Collision generating barcode code: {Code} (attempt {Attempt})", uniqueCode, attempts);
        } while (attempts < maxAttempts);

        if (attempts >= maxAttempts)
            return Result.Fail<Barcode>("Unable to generate a unique barcode.", "barcode_unique_generation_failed");

        var entity = new Barcode
        {
            UniqueCode = uniqueCode,
            Active = true,
            ProductId = erp.ProductId
        };

        await _context.Set<Barcode>().AddAsync(entity, ct);

        try
        {
            await _context.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error saving barcode.");
            return Result.Fail<Barcode>("Error saving barcode.", "barcode_persistence_error");
        }

        return Result.Success(entity);
    }
}
