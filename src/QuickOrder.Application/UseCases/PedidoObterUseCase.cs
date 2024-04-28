

using QuickOrderPedido.Application.Dtos;
using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;

namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoObterUseCase : IPedidoObterUseCase
    {
        //private readonly IPedidoGateway _pedidoGateway;
        //private readonly IPedidoStatusGateway _pedidoStatusGateway;
        //private readonly IPagamentoStatusGateway _pagamentoStatusGateway;

        //public PedidoObterUseCase(IPedidoGateway pedidoGateway, IPedidoStatusGateway pedidoStatusGateway, IPagamentoStatusGateway pagamentoStatusGateway)
        //{
        //    _pedidoGateway = pedidoGateway;
        //    _pedidoStatusGateway = pedidoStatusGateway;
        //    _pagamentoStatusGateway = pagamentoStatusGateway;
        //}

        //public async Task<ServiceResult<List<PedidoStatus>>> ConsultarFilaPedidos()
        //{
        //    var result = new ServiceResult<List<PedidoStatus>>();
        //    try
        //    {
        //        var fila = await _pedidoStatusGateway.GetAll();
        //        fila = fila.Where(x => !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Pago))
        //               && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PendentePagamento))
        //               && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Finalizado))
        //               && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Criado)))
        //                .OrderByDescending(x => (int)(EStatusPedido)Enum.Parse(typeof(EStatusPedido), x.StatusPedido)).OrderBy(x => x.DataAtualizacao);

        //        result.Data = fila.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}

        //public async Task<ServiceResult<PedidoDto>> ConsultarPedido(int id)
        //{
        //    var result = new ServiceResult<PedidoDto>();
        //    try
        //    {
        //        var pedido = await _pedidoGateway.GetFirst(id);


        //        var fila = await _pedidoStatusGateway.GetValue("NumeroPedido", id.ToString());


        //        if (pedido == null || fila == null)
        //        {
        //            result.AddError("Pedido não localizado");
        //            return result;
        //        }

        //        var pedidoDto = new PedidoDto
        //        {
        //            NumeroCliente = (int)pedido?.ClienteId,
        //            NumeroPedido = pedido.NumeroPedido,
        //            DataHoraInicio = pedido.DataHoraInicio,
        //            DataHoraFinalizado = pedido.DataHoraFinalizado,
        //            Observacao = pedido.Observacao,
        //            PedidoPago = pedido.PedidoPago,
        //            ValorPedido = pedido.ValorPedido,
        //            ProdutoPedido = SetListaProdutos(pedido?.ProdutosItemsPedido),
        //            StatusPedido = fila.StatusPedido,
        //            CarrinhoId = pedido.CarrinhoId,
        //        };

        //        result.Data = pedidoDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}

        //public async Task<ServiceResult<List<PedidoDto>>> ConsultarListaPedidos()
        //{
        //    var result = new ServiceResult<List<PedidoDto>>();
        //    try
        //    {
        //        var pedido = _pedidoGateway.ObterListaPedidos().Result;

        //        var fila = await _pedidoStatusGateway.GetAll();

        //        fila = fila.Where(x => !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Pago))
        //               && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PendentePagamento))
        //               && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Finalizado))
        //               && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Criado)))
        //                .OrderByDescending(x => (int)(EStatusPedido)Enum.Parse(typeof(EStatusPedido), x.StatusPedido)).OrderBy(x => x.DataAtualizacao);

        //        var listaPedidos = new List<PedidoDto>();

        //        if (pedido == null || fila == null)
        //        {
        //            result.AddError("Pedidos não localizado");
        //            return result;
        //        }

        //        foreach (var item in fila)
        //        {
        //            var pedidoFila = pedido?.Where(x => x.NumeroPedido.Equals(item.NumeroPedido)).FirstOrDefault();

        //            var pedidoDto = new PedidoDto
        //            {
        //                NumeroCliente = (int)pedidoFila?.ClienteId,
        //                NumeroPedido = pedidoFila.NumeroPedido,
        //                DataHoraInicio = pedidoFila.DataHoraInicio,
        //                DataHoraFinalizado = pedidoFila.DataHoraFinalizado,
        //                Observacao = pedidoFila.Observacao,
        //                PedidoPago = pedidoFila.PedidoPago,
        //                ValorPedido = pedidoFila.ValorPedido,
        //                ProdutoPedido = SetListaProdutos(pedidoFila?.ProdutosItemsPedido),
        //                StatusPedido = item.StatusPedido,
        //                CarrinhoId = pedidoFila.CarrinhoId,
        //            };

        //            listaPedidos.Add(pedidoDto);

        //        }
        //        result.Data = listaPedidos;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}

        //private List<ProdutoPedidoDto> SetListaProdutos(List<ProdutoItemPedidoEntity> produtosItemsPedido)
        //{
        //    var listProdutoPedidoDto = new List<ProdutoPedidoDto>();
        //    if (produtosItemsPedido != null)
        //    {
        //        foreach (var item in produtosItemsPedido)
        //        {
        //            var produtoPedidoDto = new ProdutoPedidoDto
        //            {
        //                NomeProduto = item.ProdutoItem.Produto.Nome.Nome,
        //                Quantidade = item.ProdutoItem.Quantidade,
        //                Valor = item.ProdutoItem.Quantidade * Convert.ToInt32(item.ProdutoItem.Produto.Preco)
        //            };
        //            listProdutoPedidoDto.Add(produtoPedidoDto);
        //        }
        //    }
        //    return listProdutoPedidoDto;
        //}
        //public async Task<ServiceResult<PagamentoDto>> ConsultarStatusPagamentoPedido(int id)
        //{
        //    var result = new ServiceResult<PagamentoDto>();
        //    try
        //    {
        //        var pagamento = await _pagamentoStatusGateway.GetValue("NumeroPedido", id.ToString());

        //        if (pagamento == null)
        //        {
        //            result.AddError("Nenhum pagamento localizado");
        //            return result;
        //        }
        //        var pagamentoDto = new PagamentoDto
        //        {
        //            NumeroPedido = pagamento.NumeroPedido,
        //            DataHora = pagamento.DataAtualizacao,
        //            StatusPagamento = pagamento.StatusPagamento,
        //        };

        //        result.Data = pagamentoDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AddError(ex.Message);
        //    }
        //    return result;
        //}
        public Task<ServiceResult<List<PedidoDto>>> ConsultarListaPedidos()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PedidoDto>> ConsultarPedido(int id)
        {
            throw new NotImplementedException();
        }
    }
}
