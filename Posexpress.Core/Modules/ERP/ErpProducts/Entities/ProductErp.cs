namespace Posexpress.Core.Modules.ERP.ErpProducts.Entities;

using Posexpress.Core.Modules.Express.ExpProducts.Entities; 
using Posexpress.Core.Modules.ERP.Barcodes.Entities;         

public class ErpProduct
{
    public int ProductId { get; set; }
    public ExpProduct Product { get; set; } = default!;
    public string UniqueCode { get; set; } = default!;     
    public decimal Cost { get; set; }
    public int Stock { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public ICollection<Barcode> Barcodes { get; set; } = new List<Barcode>();
}
