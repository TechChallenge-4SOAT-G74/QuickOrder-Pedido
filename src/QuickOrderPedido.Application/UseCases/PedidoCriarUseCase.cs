using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Domain.Enums;

namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoCriarUseCase : IPedidoCriarUseCase
    {
        private readonly ICarrinhoGateway _carrinhoGateway;
        private readonly IPedidoStatusGateway _pedidoStatusGateway;
        private readonly IPedidoGateway _pedidoGateway;

        public PedidoCriarUseCase(ICarrinhoGateway carrinhoGateway, IPedidoStatusGateway pedidoStatusGateway, IPedidoGateway pedidoGateway)
        {
            _carrinhoGateway = carrinhoGateway;
            _pedidoStatusGateway = pedidoStatusGateway;
            _pedidoGateway = pedidoGateway;
        }

        public async Task<ServiceResult> CriarPedido(int numeroCliente, ProdutoCarrinho? produtoCarrinho = null)
        {
            var result = new ServiceResult();
            try
            {
                var carrinho = numeroCliente > 0  ? await _carrinhoGateway.GetValue("NumeroCliente", numeroCliente) : new Carrinho();
                var dataCriacaoPedido = DateTime.Now;

                if (carrinho == null)
                {
                    await SaveCarrinho(numeroCliente, produtoCarrinho);
                    await SavePedido(numeroCliente, null, dataCriacaoPedido);
                }
                await SavePedidoStatus(numeroCliente, dataCriacaoPedido);

            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        private async Task SaveCarrinho(int numeroCliente, ProdutoCarrinho? produtoCarrinho)
        {
            var sacola = new Carrinho(numeroCliente, 0, DateTime.Now, produtoCarrinho == null ? null : new List<ProdutoCarrinho>() { produtoCarrinho });
            await _carrinhoGateway.Create(sacola);
        }

        private async Task SavePedidoStatus(int numeroCliente, DateTime dataCriacaoPedido)
        {
            var pedidoResult = await _pedidoGateway.GetAll();
            var codigoPedido = pedidoResult.FirstOrDefault(x => x.ClienteId == numeroCliente && x.PedidoPago.Equals(false)).Id.ToString();

            var pedidoStatus = _pedidoStatusGateway.GetValue("CodigoPedido", codigoPedido).Result;
            if (pedidoStatus == null)
            {
                await _pedidoStatusGateway.Create(new PedidoStatus(codigoPedido, EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Criado), dataCriacaoPedido));
            }
            else _pedidoStatusGateway.Update(pedidoStatus);
        }

        private async Task SavePedido(int numeroCliente, ProdutoCarrinho? produtoCarrinho, DateTime dataCriacaoPedido)
        {
            var carrinhoId = _carrinhoGateway.GetValue("NumeroCliente", numeroCliente).Id.ToString();

            var pedido = new Pedido(
                dataCriacaoPedido,
                null,
                numeroCliente,
                carrinhoId,
                produtoCarrinho == null ? 0 : produtoCarrinho.ValorProduto * produtoCarrinho.Quantidade,
                false,
                produtoCarrinho == null ? null : new List<ProdutoCarrinho>() { produtoCarrinho },
                null);

            await _pedidoGateway.Create(pedido);
        }
    }
}
