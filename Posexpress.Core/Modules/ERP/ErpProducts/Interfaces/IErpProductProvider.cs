using Corenexus.Core.Bases.Dtos;
using Posexpress.Core.Modules.ERP.ErpProducts.Dtos.Commands;
using Posexpress.Core.Modules.ERP.ErpProducts.Entities;

namespace Posexpress.Core.Modules.ERP.ErpProducts.Interfaces;

public interface IErpProductProvider
{
    Task<Result<ErpProduct>> SaveErpProductAsync(CreateErpProductCommand command, CancellationToken ct);
}
