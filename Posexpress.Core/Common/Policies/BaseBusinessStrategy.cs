namespace Posexpress.Core.Common.Policies;
public sealed class BaseBusinessStrategy : IBusinessStrategy
{
    public decimal CalculatePrice(decimal cost) => Math.Round(cost * 1.5m, 2);
    public decimal DiscountPercent(int catCount) => catCount == 1 ? 0.30m : 0m;
    public bool CanSell(int stock, int qty) => stock >= qty;
}