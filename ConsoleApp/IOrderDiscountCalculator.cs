namespace Ssdev_Cs2_ConsoleApp;

public interface IOrderDiscountCalculator
{
    decimal Do(List<Item> order);
}
