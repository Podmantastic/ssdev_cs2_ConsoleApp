namespace Ssdev_Cs2_ConsoleApp.DiscountStrategies;

public interface IDiscountStrategy
{
    /// <summary>
    /// Calculates the discount for a given order.
    /// </summary>
    /// <param name="order">A list of items in the order.</param>
    /// <returns>The total discount amount for the order.</returns>
    decimal CalculateDiscount(List<Item> order);
}
