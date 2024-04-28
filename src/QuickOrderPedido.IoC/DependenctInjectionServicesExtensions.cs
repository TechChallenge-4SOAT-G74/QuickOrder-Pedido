using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QuickOrderPedido.IoC
{
    public static class DependenctInjectionServicesExtensions
    {
        public static void AddImplementations(this IServiceCollection services,
            ServiceLifetime serviceLifetime,
            Type baseInterface,
            IEnumerable<Type>? assemblyTypesParam = null)
        {
            var assemblyTypes = assemblyTypesParam ?? baseInterface.Assembly.GetNoAbstractTypes();

            foreach (var assemblyType in assemblyTypes)
            {
                var baseInterfaceType = baseInterface;
                var typeInterfaces = assemblyType.GetInterfaces();
                var implementsInterface = typeInterfaces.Any(p => p.AssemblyQualifiedName == baseInterfaceType.AssemblyQualifiedName);

                if (implementsInterface)
                {
                    var typeInterface = typeInterfaces.FirstOrDefault(t => t.GetInterfaces().Contains(baseInterfaceType));

                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(typeInterface, assemblyType);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(typeInterface, assemblyType);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(typeInterface, assemblyType);
                            break;
                        default:
                            break;
                    }
                }

            }
        }
    }

    public static class AssemblyExtentions
    {
        public static IEnumerable<Type> GetNoAbstractTypes(this Assembly assemblyFile)
        {
            var types = assemblyFile.GetTypes();
            var assemblyTypes = types.Where(t => !t.GetTypeInfo().IsAbstract);
            return assemblyTypes;
        }
    }
}
