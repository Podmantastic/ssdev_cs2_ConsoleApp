namespace ConsoleApp;

public interface IOrderDiscountCalculator
{
    decimal CalculateTotalPrice(List<Item> order);
    // TODO: calculate the discount for the order
}
