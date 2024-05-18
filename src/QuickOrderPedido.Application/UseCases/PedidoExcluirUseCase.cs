using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Domain.Enums;


namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoExcluirUseCase : IPedidoExcluirUseCase
    {
        private readonly ICarrinhoGateway _carrinhoGateway;
        private readonly IPedidoStatusGateway _pedidoStatusGateway;
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IPedidoAtualizarUseCase _pedidoAtualizarUseCase;


        public PedidoExcluirUseCase(ICarrinhoGateway carrinhoGateway, 
            IPedidoStatusGateway pedidoStatusGateway, 
            IPedidoGateway pedidoGateway, 
            IPedidoAtualizarUseCase pedidoAtualizarUseCase)
        {
            _carrinhoGateway = carrinhoGateway;
            _pedidoStatusGateway = pedidoStatusGateway;
            _pedidoGateway = pedidoGateway;
            _pedidoAtualizarUseCase = pedidoAtualizarUseCase;
        }

        public async Task<ServiceResult> CancelarPedido(string codigoPedido, string statusPedido)
        {
            var result = new ServiceResult();
            try
            {
                var pedido = await _pedidoGateway.GetValue("CodigoPedido", codigoPedido);

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

                    await _pedidoAtualizarUseCase.AlterarStatusPedido(codigoPedido, statusPedido);

                    await LimparCarrinho(codigoPedido);
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> LimparCarrinho(string codigoPedido)
        {
            var result = new ServiceResult();
            try
            {
                var carrinho = codigoPedido != null ? await _carrinhoGateway.GetValue("CodigoPedido", codigoPedido) : new Carrinho();

                if (carrinho == null)
                {
                    result.AddError("Pedido não encontrado.");
                    return result;
                }
                _carrinhoGateway.Delete(carrinho.Id.ToString());
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }
    }
}
