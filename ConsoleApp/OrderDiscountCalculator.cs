using Ssdev_Cs2_ConsoleApp.DiscountStrategies;

namespace Ssdev_Cs2_ConsoleApp;

public class OrderDiscountCalculator : IOrderDiscountCalculator
{
    private readonly IEnumerable<IDiscountStrategy> discountStrategies;

    public OrderDiscountCalculator(IEnumerable<IDiscountStrategy> discountStrategies)
    {
        this.discountStrategies = discountStrategies;
    }

    /// <summary>
    /// Calculates the total discount for a given order using various discount strategies.
    /// </summary>
    /// <param name="order">A list of items representing the order.</param>
    /// <returns>The total discount for the order, rounded to two decimal places.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the order is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the order contains invalid items.</exception>
    public decimal Do(List<Item> order)
    {
        ValidateOrderItems(order);

        decimal totalDiscount = discountStrategies.Sum(strategy => strategy.CalculateDiscount(order));

        return Math.Round(totalDiscount, 2);
    }


    private static void ValidateOrderItems(List<Item> order)
    {
        // Check for null or empty list
        if (order == null) throw new ArgumentNullException(nameof(order), "Order items list cannot be null.");

        if (order.Count == 0) throw new ArgumentException("Order items list cannot be empty.", nameof(order));

        // Validate each order item
        foreach (var item in order)
        {
            // Check item name
            if (string.IsNullOrWhiteSpace(item.Name)) throw new ArgumentException("Item name cannot be null or empty.", nameof(item.Name));

            // Check unit price
            if (item.UnitPrice < 0) throw new ArgumentException($"Unit price for {item.Name} cannot be negative.", nameof(item.UnitPrice));

            // Check quantity
            if (item.Quantity < 0) throw new ArgumentException($"Quantity for {item.Name} cannot be negative.", nameof(item.Quantity));
        }
    }
}