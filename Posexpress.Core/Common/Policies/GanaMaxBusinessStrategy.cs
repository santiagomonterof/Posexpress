namespace Posexpress.Core.Common.Policies;
public sealed class GanaMaxBusinessStrategy : IBusinessStrategy
{
    public decimal CalculatePrice(decimal cost) => Math.Round(cost * 1.8m, 2);
    public decimal DiscountPercent(int catCount) => catCount == 1 ? 0.10m : 0m;
    public bool CanSell(int stock, int qty) => (stock - qty) > 10;
}