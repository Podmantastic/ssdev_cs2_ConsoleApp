namespace ConsoleApp;

public class OrderDiscountCalculator : IOrderDiscountCalculator
{
    public decimal CalculateTotalPrice(List<Item> order)
    {
        if (order == null)
        {
            throw new ArgumentException("The order list cannot be null.");
        }

        if (order.Count == 0)
        {
            return 0.00m;
        }

        decimal totalPrice = 0;

        // Step 1: Calculate the total price for each item
        foreach (var item in order)
        {
            // Input validation: Check if the item is null
            if (item == null)
            {
                throw new ArgumentException("The order list contains a null item.");
            }

            // Input validation: Check if quantity or unit price is negative
            if (item.Quantity < 0 || item.UnitPrice < 0)
            {
                throw new ArgumentException("Quantity and unit price must be non-negative.");
            }

            decimal itemTotal = item.Quantity * item.UnitPrice;

            // Apply bulk discount if applicable
            if (item.Quantity >= 3)
            {
                itemTotal *= 0.90m; // 10% discount
            }

            totalPrice += itemTotal;
        }

        // Step 2: Apply order total discount if applicable
        if (totalPrice > 100)
        {
            totalPrice *= 0.95m; // 5% discount
        }

        // Step 3: Manually round to 2 decimal places
        totalPrice = RoundToTwoDecimalPlaces(totalPrice);

        return totalPrice;
    }

    private static decimal RoundToTwoDecimalPlaces(decimal value)
    {
        decimal truncated = Math.Truncate(value * 100) / 100;
        decimal remainder = value - truncated;
        if (remainder > 0.005m)
        {
            truncated += 0.01m;
        }
        return truncated;
    }
}