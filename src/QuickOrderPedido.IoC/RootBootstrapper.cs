using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            services.AddSingleton(typeof(IRabbitMqPub<>), typeof(RabbitMqPub<>));
            services.AddScoped<IProcessaEvento, ProcessaEvento>();

            //Repositories MongoDB
            services.AddSingleton<IMondoDBContext, MondoDBContext>();
            services.AddScoped<ICarrinhoGateway, CarrinhoGateway>();
            services.AddScoped<IPedidoGateway, PedidoGateway>();
            services.AddScoped<IPedidoStatusGateway, PedidoStatusGateway>();

            //UseCases
            services.AddScoped<IPedidoAtualizarUseCase, PedidoAtualizarUseCase>();
            services.AddScoped<IPedidoExcluirUseCase, PedidoExcluirUseCase>();
            services.AddScoped<IPedidoCriarUseCase, PedidoCriarUseCase>();
            services.AddScoped<IPedidoObterUseCase, PedidoObterUseCase>();


        }
    }
}
