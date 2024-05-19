using Microsoft.Extensions.DependencyInjection;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using System.Text.Json;

namespace QuickOrderPedido.Infra.MQ
{
    public class ProcessaEvento : IProcessaEvento
    {
        //private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessaEvento(IServiceScopeFactory scopeFactory)
        {
            //_mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void Processa(string mensagem)
        {

            using var scope = _scopeFactory.CreateScope();

            var itemRepository = scope.ServiceProvider.GetRequiredService<IPedidoGateway>();

            var restauranteRead = JsonSerializer.Deserialize<Pedido>(mensagem);

            //var restaurante = _mapper.Map<Pedido>(restauranteReadDto);

            //if (!itemRepository.ExisteRestauranteExterno(restaurante.Id))
            //{
            //    itemRepository.CreateRestaurante(restaurante);
            //    itemRepository.SaveChanges();
            //}
        }
    }
}
