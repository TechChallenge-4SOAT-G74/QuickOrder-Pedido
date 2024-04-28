
using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoAtualizarUseCase : IPedidoAtualizarUseCase
    {
        //private readonly ICarrinhoGateway _carrinhoGateway;

        //public PedidoAtualizarUseCase(ICarrinhoGateway carrinhoGateway)
        //{
        //    _carrinhoGateway = carrinhoGateway;
        //}

        //public async Task<ServiceResult> AlterarItemAoPedido(string id, List<ProdutoCarrinho> produtoCarrinho)
        //{
        //    var result = new ServiceResult();
        //    try
        //    {
        //        var carrinho = await _carrinhoGateway.GetValue("NumeroPedido", id);

        //        if (carrinho == null)
        //        {
        //            result.AddError("Pedido não localizado.");
        //            return result;
        //        }

        //        carrinho.DataAtualizacao = DateTime.Now;
        //        carrinho.Valor = produtoCarrinho.Sum(x => x.ValorProduto);
        //        carrinho.ProdutosCarrinho = produtoCarrinho;

        //        _carrinhoGateway.Update(carrinho);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}

        //public async Task<ServiceResult> AlterarStatusPedido(int id, string pedidoStatus)
        //{
        //    var result = new ServiceResult();
        //    try
        //    {
        //        var pedido = await _pedidoStatusGateway.GetValue("NumeroPedido", id.ToString());

        //        if (pedido == null)
        //        {
        //            result.AddError("Pedido não localizado.");
        //            return result;
        //        }

        //        pedido.StatusPedido = pedidoStatus;
        //        pedido.DataAtualizacao = DateTime.Now;
        //        _pedidoStatusGateway.Update(pedido);

        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}

        //public async Task<ServiceResult> ConfirmarPagamentoPedido(int id)
        //{
        //    var result = new ServiceResult();
        //    try
        //    {
        //        var pedido = await _pedidoGateway.GetFirst(id);

        //        if (pedido == null)
        //        {
        //            result.AddError("Pedido não localizado.");
        //            return result;
        //        }

        //        pedido.PedidoPago = true;

        //        await _pedidoGateway.Update(pedido);

        //        var sacolaDto = new SacolaDto { NumeroCliente = pedido.ClienteId.ToString(), NumeroPedido = pedido.NumeroPedido.ToString(), CarrinhoId = pedido.CarrinhoId, Valor = pedido.ValorPedido };
        //        await _pagamentoUseCase.AtualizarStatusPagamento(pedido.NumeroPedido.ToString(), (int)EStatusPagamento.Aprovado);

        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}

        //public async Task<ServiceResult<PaymentQrCodeResponse>> ConfirmarPedido(int id)
        //{
        //    var result = new ServiceResult<PaymentQrCodeResponse>();
        //    try
        //    {
        //        var pedido = await _pedidoGateway.GetFirst(id);
        //        var carrinho = await _carrinhoGateway.GetValue("NumeroPedido", id.ToString());

        //        if (pedido == null || carrinho == null)
        //        {
        //            result.AddError("Pedido não localizado.");
        //            return result;
        //        }

        //        var produtosItems = new List<ProdutosItemsPedidoEntity>();

        //        foreach (var item in carrinho.ProdutosCarrinho)
        //            produtosItems.Add(new ProdutosItemsPedidoEntity(new ProdutoItem(item.IdProduto, 1), id));

        //        pedido.ProdutosItemsPedido = produtosItems;
        //        pedido.ValorPedido = carrinho.ProdutosCarrinho.Sum(x => x.ValorProduto);

        //        await _pedidoGateway.Update(pedido);

        //        var pedidoStatusExiste = await _pedidoStatusGateway.GetValue("NumeroPedido", id.ToString());

        //        if (pedidoStatusExiste == null)
        //        {
        //            var pedidoStatus = new PedidoStatus(
        //                pedido.NumeroPedido,
        //                EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PendentePagamento),
        //                DateTime.Now);

        //            await _pedidoStatusGateway.Create(pedidoStatus);
        //        }
        //        else
        //        {
        //            pedidoStatusExiste.StatusPedido = EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PendentePagamento);
        //            _pedidoStatusGateway.Update(pedidoStatusExiste);
        //        }

        //        result = await _pagamentoUseCase.GerarQrCodePagamento(pedido.NumeroPedido);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}
        public Task<ServiceResult> AlterarItemAoPedido(string id, List<ProdutoCarrinho> pedidoDto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> AlterarStatusPedido(int id, string pedidoStatus)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> ConfirmarPedido(int id)
        {
            throw new NotImplementedException();
        }
    }
}
