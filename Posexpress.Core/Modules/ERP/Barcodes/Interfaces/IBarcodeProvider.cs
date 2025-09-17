using Corenexus.Core.Bases.Dtos;
using Posexpress.Core.Modules.ERP.Barcodes.Dtos.Commands;
using Posexpress.Core.Modules.ERP.Barcodes.Entities;

namespace Posexpress.Core.Modules.ERP.Barcodes.Interfaces;

public interface IBarcodeProvider
{
    Task<Result<Barcode>> AssignBarcodeAsync(AssignBarcodeCommand command, CancellationToken ct);
}
