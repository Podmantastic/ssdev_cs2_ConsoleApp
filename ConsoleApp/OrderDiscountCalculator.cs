namespace Ssdev_Cs2_ConsoleApp;

public class OrderDiscountCalculator : IOrderDiscountCalculator
{
    public decimal Do(List<Item> order)
    {
        ValidateOrderItems(order);

        // Calculate total order value before discounts
        decimal totalOrderValue = order.Sum(item => item.UnitPrice * item.Quantity);

        // Rule 1: 10% off items with 3 or more units
        var itemDiscounts = order
            .Where(item => item.Quantity >= 3)
            .Select(item =>
            {
                decimal itemDiscount = item.UnitPrice * 0.10m;
                return itemDiscount;
            })
            .Sum();

        // Rule 2: 5% off entire order if total exceeds $100
        decimal orderLevelDiscount = totalOrderValue > 100
            ? totalOrderValue * 0.05m
            : 0;

        return Math.Round(itemDiscounts + orderLevelDiscount, 2);
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