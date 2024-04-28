

using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;

namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoCriarUseCase : IPedidoCriarUseCase
    {
        private readonly ICarrinhoGateway _carrinhoGateway;

        public PedidoCriarUseCase(ICarrinhoGateway carrinhoGateway)
        {
            _carrinhoGateway = carrinhoGateway;
        }

        public async Task<ServiceResult> CriarPedido(int? numeroCliente = null)
        {
            //TODO: Usaundo número do cliente como parametro até cria a autenticação.

            var result = new ServiceResult();
            try
            {

                //var numeroPedido = _pedidoGateway.GetAll().Result.Select(x => x.Id).OrderByDescending(x => x).FirstOrDefault() + 1;
                //var carrinho = new Carrinho(numeroPedido, numeroCliente, 0, DateTime.Now, null);
                //var pedido = new PedidoEntity(numeroPedido, DateTime.Now, null, numeroCliente, carrinho.Id.ToString(), null, 0, false);
                //var pedidoStatus = new PedidoStatus(numeroPedido, EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Criado), DateTime.Now);

                //await _carrinhoGateway.Create(carrinho);
                //await _pedidoStatusGateway.Create(pedidoStatus);
                //await _pedidoGateway.Insert(pedido);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }
    }
}
