using System.Globalization;

namespace Ssdev_Cs2_ConsoleApp;

public class OrderProcessor(IOrderDiscountCalculator discountCalulator) : IOrderProcessor
{
    private readonly IOrderDiscountCalculator discountCalulator = discountCalulator;

    public void Do()
    {
        List<Item> order = [
            new("Laptop", 1, 1000.00m),
            new("Mouse", 3, 25.00m),
            new("Keyboard", 2, 50.00m)];

        var totalPriceBeforeDiscount = order.Sum(item => item.Quantity * item.UnitPrice);
        Console.WriteLine($"Order total before discount: {totalPriceBeforeDiscount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}");

        var totalPriceAfterDiscount = totalPriceBeforeDiscount - discountCalulator.Do(order);
        Console.WriteLine($"Order total after discount: {totalPriceAfterDiscount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}");
    }
}
