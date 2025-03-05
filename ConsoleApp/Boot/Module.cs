using Ninject.Modules;

namespace Ssdev_Cs2_ConsoleApp.Boot;

public class Module : NinjectModule
{
    public override void Load()
    {
        // Entry point
        Bind<IOrderProcessor>().To<OrderProcessor>().InSingletonScope();

        Bind<IOrderDiscountCalculator>().To<OrderDiscountCalculator>().InSingletonScope();
    }
}