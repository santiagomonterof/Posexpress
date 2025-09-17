using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Posexpress.Core.Modules.ERP.Barcodes.Dtos.Commands;
using Posexpress.Core.Modules.ERP.Barcodes.Interfaces;
using Posexpress.Core.Modules.ERP.Barcodes.Dtos.Results;

namespace Posexpress.Api.Endpoints.ERP.Barcodes;

public class PostAssignBarcode(IBarcodeProvider provider) : EndpointBaseAsync
    .WithRequest<AssignBarcodeCommand>
    .WithActionResult<BarcodeResult>
{
    [HttpPost("api/v{version:apiVersion}/erp/barcodes")]
    [ApiVersion("1")]
    [SwaggerOperation(Tags = ["ERP"])]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BarcodeResult))]
    public override async Task<ActionResult<BarcodeResult>> HandleAsync(
        AssignBarcodeCommand request, CancellationToken ct = default)
    {
        var result = await provider.AssignBarcodeAsync(request, ct);
        if (result.Failure) return Problem(detail: result.Error, title: result.ErrorKey);

        return Ok(BarcodeResult.FromEntity(result.Value));
    }
}
