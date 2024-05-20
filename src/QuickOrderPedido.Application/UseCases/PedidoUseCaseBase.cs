using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoUseCaseBase
    {
        private readonly ICarrinhoGateway _carrinhoGateway;
        private readonly IPedidoStatusGateway _pedidoStatusGateway;

        public PedidoUseCaseBase(ICarrinhoGateway carrinhoGateway,
                IPedidoStatusGateway pedidoStatusGateway)
        {
            _carrinhoGateway = carrinhoGateway;
            _pedidoStatusGateway = pedidoStatusGateway;
        }

        public async Task<ServiceResult> AlterarStatusPedido(string codigoPedido, string pedidoStatus)
        {
            var result = new ServiceResult();
            try
            {
                var pedido = await _pedidoStatusGateway.GetValue("CodigoPedido", codigoPedido);

                if (pedido == null)
                {
                    result.AddError("Pedido não localizado.");
                    return result;
                }

                pedido.StatusPedido = pedidoStatus;
                pedido.DataAtualizacao = DateTime.Now;
                _pedidoStatusGateway.Update(pedido);

            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> LimparCarrinho(int numeroCliente)
        {
            var result = new ServiceResult();
            try
            {
                var carrinho = numeroCliente != null ? await _carrinhoGateway.GetValue("NumeroCliente", numeroCliente) : new Carrinho();

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