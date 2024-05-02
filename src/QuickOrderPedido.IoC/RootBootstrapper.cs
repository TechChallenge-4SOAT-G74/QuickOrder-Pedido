using Microsoft.Extensions.DependencyInjection;
using QuickOrderPedido.Application.Events;
using QuickOrderPedido.Application.UseCases;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Infra.Gateway;
using QuickOrderPedido.Infra.Gateway.Core;
using QuickOrderPedido.Infra.MQ;

namespace QuickOrderPedido.IoC
{
    public static class RootBootstrapper
    {
        public static void BootstrapperRegisterServices(this IServiceCollection services)
        {
            var assemblyTypes = typeof(RootBootstrapper).Assembly.GetNoAbstractTypes();

            services.AddImplementations(ServiceLifetime.Scoped, typeof(IBaseUseCase), assemblyTypes);

            services.AddSingleton(typeof(IRabbitMqPub<>), typeof(RabbitMqPub<>));
            services.AddSingleton<IProcessaEvento, ProcessaEvento>();

            //Repositories MongoDB
            services.AddSingleton<IMondoDBContext, MondoDBContext>();
            services.AddScoped<ICarrinhoGateway, CarrinhoGateway>();


            //UseCases
            services.AddScoped<IPedidoAtualizarUseCase, PedidoAtualizarUseCase>();
            services.AddScoped<IPedidoExcluirUseCase, PedidoExcluirUseCase>();
            services.AddScoped<IPedidoCriarUseCase, PedidoCriarUseCase>();
            services.AddScoped<IPedidoObterUseCase, PedidoObterUseCase>();


        }
    }
}
