using Posexpress.Core.Modules.ERP.Barcodes.Entities;

namespace Posexpress.Core.Modules.ERP.Barcodes.Dtos.Results;

public record BarcodeResult(int Id, string UniqueCode, int ErpProductId)
{
    public static BarcodeResult FromEntity(Barcode b) =>
        new(b.Id, b.UniqueCode, b.ProductId); 
}
