using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Domain.Adapters
{
    public interface IPedidoStatusGateway : IBaseGateway, IBaseMongoDBRepository<PedidoStatus>
    {
    }
}
