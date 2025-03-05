using Ssdev_Cs2_ConsoleApp.Discounts;

namespace Ssdev_Cs2_ConsoleApp.DiscountStrategies;

public class OrderTotalDiscountStrategy(IDiscountOptions discountOptions) : IDiscountStrategy
{
    private readonly IDiscountOptions discountOptions = discountOptions;

    /// <summary>
    /// Calculates the discount for an order based on the total order value.
    /// </summary>
    /// <param name="order">A list of items in the order.</param>
    /// <returns>
    /// The discount amount if the total order value exceeds 100; otherwise, returns 0.
    /// </returns>
    public decimal CalculateDiscount(List<Item> order)
    {
        decimal totalOrderValue = order.Sum(item => item.UnitPrice * item.Quantity);
        return totalOrderValue > 100 ? totalOrderValue * discountOptions.BulkOrderDiscount : 0;
    }
}