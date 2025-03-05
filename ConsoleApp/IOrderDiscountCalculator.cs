namespace ConsoleApp;

public interface IOrderDiscountCalculator
{
    /// <summary>
    /// Calculates the total price of the given order.
    /// </summary>
    /// <param name="order">A list of items representing the order.</param>
    /// <returns>The total price of the order as a decimal.</returns>
    decimal CalculateTotalPrice(List<Item> order);
    
    // TODO: calculate the discount for the order
}
