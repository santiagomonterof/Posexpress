using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Posexpress.Core.Modules.ERP.ErpProducts.Dtos.Commands;
using Posexpress.Core.Modules.ERP.ErpProducts.Entities;
using Posexpress.Core.Modules.ERP.ErpProducts.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Posexpress.Api.Endpoints.ERP.ErpProducts;

public class PostCreateErpProduct(IErpProductProvider provider) : EndpointBaseAsync
  .WithRequest<CreateErpProductCommand>
  .WithActionResult<ErpProduct>
{
    [HttpPost("api/v{version:apiVersion}/erp/products")]
    [ApiVersion("1")]
    [SwaggerOperation(Tags = ["ERP"])]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ErpProduct))]
    public override async Task<ActionResult<ErpProduct>> HandleAsync(
        CreateErpProductCommand request,
        CancellationToken ct = default)
    {
        var result = await provider.SaveErpProductAsync(request, ct);
        if (result.Failure) return Problem(detail: result.Error, title: result.ErrorKey);
        return Ok(result.Value);
    }
}
