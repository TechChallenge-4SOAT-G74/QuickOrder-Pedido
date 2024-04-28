using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Infra.Core;

namespace QuickOrderPedido.Infra.Gateway
{
    public class CarrinhoGateway : BaseMongoDBRepository<Carrinho>, ICarrinhoGateway
    {
        public CarrinhoGateway(IMondoDBContext mondoDBContext) : base(mondoDBContext)
        {
        }
    }
}
