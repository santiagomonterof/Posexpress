namespace Posexpress.Core.Common.Policies;
public interface IBusinessStrategy
{
    decimal CalculatePrice(decimal cost);
    decimal DiscountPercent(int categoriesCount);
    bool CanSell(int currentStock, int quantity);
}