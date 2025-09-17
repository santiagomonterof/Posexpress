namespace Posexpress.Core.Common.Services;
public sealed class SimpleUniqueCodeGenerator : IUniqueCodeGenerator
{
    public string NewProductCode() => $"PRD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}".Substring(0, 20).ToUpper();
    public string NewBarcodeCode() => $"BAR-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}".Substring(0, 20).ToUpper();
}