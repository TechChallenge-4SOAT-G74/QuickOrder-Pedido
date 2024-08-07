using Microsoft.Extensions.DependencyInjection;
using QuickOrderPedido.Application.Events;
using QuickOrderPedido.Application.UseCases;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Infra.Gateway;
using QuickOrderPedido.Infra.Gateway.Core;
using QuickOrderPedido.Infra.Gateway.Gateway;
using QuickOrderPedido.Infra.MQ;
using System.Diagnostics.CodeAnalysis;

namespace QuickOrderPedido.IoC
{
    [ExcludeFromCodeCoverage]
    public static class RootBootstrapper
    {
        public static void BootstrapperRegisterServices(this IServiceCollection services)
        {
            var assemblyTypes = typeof(RootBootstrapper).Assembly.GetNoAbstractTypes();

            services.AddHostedService<RabbitMqSub>();
            services.AddSingleton(typeof(IRabbitMqPub<>), typeof(RabbitMqPub<>));
            services.AddSingleton<IProcessaEvento, ProcessaEvento>();

            //Repositories MongoDB
            services.AddSingleton<IMondoDBContext, MondoDBContext>();
            services.AddSingleton<ICarrinhoGateway, CarrinhoGateway>();
            services.AddSingleton<IPedidoGateway, PedidoGateway>();
            services.AddSingleton<IPedidoStatusGateway, PedidoStatusGateway>();

            //UseCases
            services.AddSingleton<IPedidoAtualizarUseCase, PedidoAtualizarUseCase>();
            services.AddScoped<IPedidoExcluirUseCase, PedidoExcluirUseCase>();
            services.AddSingleton<IPedidoCriarUseCase, PedidoCriarUseCase>();
            services.AddScoped<IPedidoObterUseCase, PedidoObterUseCase>();


        }
    }
}
