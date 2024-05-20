using QuickOrderPedido.Application.Dtos;
using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Domain.Enums;

namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoObterUseCase : IPedidoObterUseCase
    {
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IPedidoStatusGateway _pedidoStatusGateway;

        public PedidoObterUseCase(IPedidoGateway pedidoGateway, IPedidoStatusGateway pedidoStatusGateway)
        {
            _pedidoGateway = pedidoGateway;
            _pedidoStatusGateway = pedidoStatusGateway;
        }

        public async Task<ServiceResult<PedidoDto>> ConsultarPedido(string id)
        {
            var result = new ServiceResult<PedidoDto>();
            try
            {
                var pedido = await _pedidoGateway.Get(id);


                var fila = await _pedidoStatusGateway.GetValue("CodigoPedido", id);


                if (pedido == null || fila == null)
                {
                    result.AddError("Pedido não localizado");
                    return result;
                }

                var pedidoDto = new PedidoDto
                {
                    NumeroCliente = pedido.ClienteId,
                    CodigoPedido = pedido.Id.ToString(),
                    DataHoraInicio = pedido.DataHoraInicio,
                    DataHoraFinalizado = pedido.DataHoraFinalizado,
                    Observacao = pedido.Observacao,
                    PedidoPago = pedido.PedidoPago,
                    ValorPedido = pedido.ValorPedido,
                    ProdutoPedido = SetListaProdutos(pedido?.Produtos),
                    StatusPedido = fila.StatusPedido,
                    CarrinhoId = pedido.CarrinhoId,
                };

                result.Data = pedidoDto;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult<List<PedidoDto>>> ConsultarListaPedidos()
        {
            var result = new ServiceResult<List<PedidoDto>>();
            try
            {
                var pedido = _pedidoGateway.GetAll().Result;


                if (pedido == null || pedido.Count() == 0)
                {
                    result.AddError("Pedidos não localizado");
                    return result;
                }

                var fila = await _pedidoStatusGateway.GetAll();

                fila = fila.Where(x => !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Pago))
                       && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PendentePagamento))
                       && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Finalizado))
                       && !x.StatusPedido.Equals(EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Criado)))
                        .OrderByDescending(x => (int)(EStatusPedido)Enum.Parse(typeof(EStatusPedido), x.StatusPedido)).OrderBy(x => x.DataAtualizacao);

                var listaPedidos = new List<PedidoDto>();

                if (fila == null || fila.Count() == 0)
                {
                    result.AddError("Pedidos não localizado");
                    return result;
                }

                foreach (var item in fila)
                {
                    var pedidoFila = pedido?.FirstOrDefault(x => x.Id.ToString().Equals(item.CodigoPedido));

                    if (pedidoFila == null)
                        result.Data = new List<PedidoDto>();

                    var pedidoDto = new PedidoDto
                    {
                        NumeroCliente = pedidoFila.ClienteId,
                        CodigoPedido = pedidoFila.Id.ToString(),
                        DataHoraInicio = pedidoFila.DataHoraInicio,
                        DataHoraFinalizado = pedidoFila.DataHoraFinalizado,
                        Observacao = pedidoFila.Observacao,
                        PedidoPago = pedidoFila.PedidoPago,
                        ValorPedido = pedidoFila.ValorPedido,
                        ProdutoPedido = SetListaProdutos(pedidoFila.Produtos),
                        StatusPedido = item.StatusPedido,
                        CarrinhoId = pedidoFila.CarrinhoId,
                    };

                    listaPedidos.Add(pedidoDto);

                }
                result.Data = listaPedidos;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        private List<ProdutoPedidoDto> SetListaProdutos(List<ProdutoCarrinho> produtoCarrinho)
        {
            var listProdutoPedidoDto = new List<ProdutoPedidoDto>();
            if (produtoCarrinho != null)
            {
                foreach (var item in produtoCarrinho)
                {
                    var produtoPedidoDto = new ProdutoPedidoDto
                    {
                        NomeProduto = item.NomeProduto,
                        Quantidade = item.Quantidade,
                        Valor = item.ValorProduto
                    };
                    listProdutoPedidoDto.Add(produtoPedidoDto);
                }
            }
            return listProdutoPedidoDto;
        }
    }
}
