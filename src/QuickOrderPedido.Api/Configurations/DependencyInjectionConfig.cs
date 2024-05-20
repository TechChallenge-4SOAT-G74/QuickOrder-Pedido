using QuickOrderPedido.IoC;
using System.Diagnostics.CodeAnalysis;

namespace QuickOrderPedido.Api.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            RootBootstrapper.BootstrapperRegisterServices(services);
        }
    }
}
