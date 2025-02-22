using System.Globalization;

namespace ConsoleApp;

public class OrderProcessor: IOrderProcessor
{
    private readonly IOrderDiscountCalculator discountCalulator;

    public OrderProcessor(IOrderDiscountCalculator discountCalulator)
    {
        this.discountCalulator = discountCalulator;
    }

    public void Do()
    {
        var order = new List<Item> {
            new("Laptop", 1, 1000.00m),
            new("Mouse", 3, 25.00m),
            new("Keyboard", 2, 50.00m)};

        var totalPrice = discountCalulator.CalculateTotalPrice(order);

        Console.WriteLine($"Order total: {totalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}");
    }
}
