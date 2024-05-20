using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;


namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoExcluirUseCase : PedidoUseCaseBase, IPedidoExcluirUseCase
    {
        private readonly ICarrinhoGateway _carrinhoGateway;
        private readonly IPedidoGateway _pedidoGateway;


        public PedidoExcluirUseCase(ICarrinhoGateway carrinhoGateway,
            IPedidoStatusGateway pedidoStatusGateway,
            IPedidoGateway pedidoGateway) : base(carrinhoGateway, pedidoStatusGateway)
        {
            _carrinhoGateway = carrinhoGateway;
            _pedidoGateway = pedidoGateway;
        }

        public async Task<ServiceResult> CancelarPedido(string codigoPedido, string statusPedido)
        {
            var result = new ServiceResult();
            try
            {
                var pedido = await _pedidoGateway.Get(codigoPedido);

                if (pedido == null)
                {
                    result.AddError("Pedido não encontrado.");
                    return result;
                }
                _pedidoGateway.Delete(pedido.Id.ToString());

                if (pedido.CarrinhoId != null)
                {
                    var carrinho = await _carrinhoGateway.Get(pedido.CarrinhoId);
                    if (carrinho != null)
                        _carrinhoGateway.Delete(carrinho.Id.ToString());

                    await AlterarStatusPedido(codigoPedido, statusPedido);

                    await LimparCarrinho(pedido.ClienteId);
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

    }
}
