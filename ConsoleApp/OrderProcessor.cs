using log4net.Core;
using log4net.Repository.Hierarchy;

namespace ConsoleApp;

public class OrderProcessor: IOrderProcessor
{
    private readonly ILogger logger;

    public OrderProcessor(ILogger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Do()
    {
        //var orderTotal = _order.Sum(item => item.Quantity * item.UnitPrice);
        var orderTotal = 0;

        logger.Log(Level.Info, $"Order total: {orderTotal:C}");

        Console.WriteLine($"Order total: {orderTotal:C}");
    }
}
