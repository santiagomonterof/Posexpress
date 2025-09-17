namespace Posexpress.Core.Modules.ERP.ErpProducts.Dtos.Commands;

public sealed record CreateErpProductCommand(
    string Name,
    int ProductTypeId,
    decimal Cost,
    int InitialStock = 0,
    string? Observations = null
);
