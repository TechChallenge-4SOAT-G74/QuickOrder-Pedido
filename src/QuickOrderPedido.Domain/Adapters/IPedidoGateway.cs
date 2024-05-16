using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Domain.Adapters
{
    public interface IPedidoGateway : IBaseGateway, IBaseMongoDBRepository<Pedido>
    {
    }
}
