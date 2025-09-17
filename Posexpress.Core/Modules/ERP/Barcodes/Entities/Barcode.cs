namespace Posexpress.Core.Modules.ERP.Barcodes.Entities;

using Posexpress.Core.Modules.ERP.ErpProducts.Entities;

public class Barcode
{
    public int Id { get; set; }
    public string UniqueCode { get; set; } = default!;
    public bool Active { get; set; } = true;
    public int ProductId { get; set; }        
    public ErpProduct Product { get; set; } = default!;
}
