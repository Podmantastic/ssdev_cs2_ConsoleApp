using Ssdev_Cs2_ConsoleApp.Discounts;
using Ssdev_Cs2_ConsoleApp.DTO;

namespace Ssdev_Cs2_ConsoleApp.DiscountStrategies;

public class BulItemDiscountStrategy(IDiscountOptions discountOptions) : IDiscountStrategy
{
    private readonly IDiscountOptions discountOptions = discountOptions;

    /// <summary>
    /// Calculates the total discount for an order based on bulk item discount strategy.
    /// </summary>
    /// <param name="order">A list of items in the order.</param>
    /// <returns>The total discount amount for items that qualify for the bulk item discount.</returns>
    public decimal CalculateDiscount(List<Item> order)
    {
        return order
            .Where(item => item.Quantity >= 3)
            .Select(item => item.UnitPrice * discountOptions.BulkItemDiscount)
            .Sum();
    }
}
