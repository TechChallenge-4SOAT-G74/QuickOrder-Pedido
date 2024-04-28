using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;


namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoExcluirUseCase : IPedidoExcluirUseCase
    {
        private readonly ICarrinhoGateway _carrinhoGateway;

        public PedidoExcluirUseCase(ICarrinhoGateway carrinhoGateway)
        {
            _carrinhoGateway = carrinhoGateway;
        }

        public async Task<ServiceResult> CancelarPedido(string carrinhoId)
        {
            var result = new ServiceResult();
            try
            {
                //var pedido = await _pedidoGateway.GetFirst(x => x.CarrinhoId.Equals(carrinhoId));

                //if (pedido == null)
                //{
                //    result.AddError("Pedido não encontrado.");
                //    return result;
                //}
                //await _pedidoGateway.Delete(pedido.Id);

                //if (pedido.CarrinhoId != null)
                //{
                //    var carrinho = await _carrinhoGateway.Get(pedido.CarrinhoId);
                //    if (carrinho != null)
                //        _carrinhoGateway.Delete(carrinho.Id.ToString());

                //    var status = await _pedidoStatusGateway.GetValue("CarrinhoId", pedido.CarrinhoId);
                //    if (status != null)
                //        _pedidoStatusGateway.Delete(status.Id.ToString());
                //}
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }
    }
}
