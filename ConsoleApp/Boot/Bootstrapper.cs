using Ninject;

namespace Ssdev_Cs2_ConsoleApp.Boot;

internal class Bootstrapper
{
    public static IOrderProcessor ResolveGenerator()
    {
        var kernel = new StandardKernel(new NinjectSettings(), new Module());

        return kernel.Get<IOrderProcessor>();
    }
}