using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Infra.Gateway.Core;

namespace QuickOrderPedido.Infra.Gateway
{
    public class PedidoGateway : BaseMongoDBRepository<Pedido>, IPedidoGateway
    {
        public PedidoGateway(IMondoDBContext mondoDBContext) : base(mondoDBContext)
        {
        }
    }
}
