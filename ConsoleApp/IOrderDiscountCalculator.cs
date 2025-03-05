namespace Ssdev_Cs2_ConsoleApp;

public interface IOrderDiscountCalculator
{
    /// <summary>
    /// Calculates the discount for a given order.
    /// </summary>
    /// <param name="order">A list of items representing the order.</param>
    /// <returns>The calculated discount as a decimal value.</returns>
    decimal Do(List<Item> order);
}
