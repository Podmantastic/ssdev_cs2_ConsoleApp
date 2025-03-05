using Ninject.Modules;
using Ssdev_Cs2_ConsoleApp.Discounts;
using Ssdev_Cs2_ConsoleApp.DiscountStrategies;

namespace Ssdev_Cs2_ConsoleApp.Boot;

public class Module : NinjectModule
{
    public override void Load()
    {
        // Entry point
        Bind<IOrderProcessor>().To<OrderProcessor>().InSingletonScope();

        Bind<IOrderDiscountCalculator>().To<OrderDiscountCalculator>().InSingletonScope();

        Bind<IDiscountOptions>().To<DiscountOptions>().InSingletonScope();

        // Discount strategies
        Bind<IDiscountStrategy>().To<BulItemDiscountStrategy>();
        Bind<IDiscountStrategy>().To<OrderTotalDiscountStrategy>();
    }
}