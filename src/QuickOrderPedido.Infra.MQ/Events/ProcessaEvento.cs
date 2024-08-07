using QQuickOrderPedido.Domain.Enums;
using QuickOrderPedido.Application.Dtos;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Domain.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace QuickOrderPedido.Infra.MQ
{
    [ExcludeFromCodeCoverage]
    public class ProcessaEvento : IProcessaEvento
    {
        private readonly ICarrinhoGateway _carrinhoGateway;
        private readonly IPedidoCriarUseCase _pedidoCriarUseCase;
        private readonly IPedidoAtualizarUseCase _pedidoAtualizarUseCase;
        private readonly IPedidoObterUseCase _pedidoObterUseCase;

        public ProcessaEvento(ICarrinhoGateway carrinhoGateway, IPedidoCriarUseCase pedidoCriarUseCase, IPedidoAtualizarUseCase pedidoAtualizarUseCase, IPedidoObterUseCase pedidoObterUseCase)
        {
            _carrinhoGateway = carrinhoGateway;
            _pedidoCriarUseCase = pedidoCriarUseCase;
            _pedidoAtualizarUseCase = pedidoAtualizarUseCase;
            _pedidoObterUseCase = pedidoObterUseCase;
        }

        public void ProcessaProduto(string mensagem)
        {
            var peditoRecebido = JsonSerializer.Deserialize<ProdutoSelecionadoDto>(mensagem);
            Console.Write(peditoRecebido);

            var produtoCarrinho = new ProdutoCarrinho(peditoRecebido.CategoriaProduto, peditoRecebido.NomeProduto, peditoRecebido.IdProduto, peditoRecebido.Quantidade, peditoRecebido.ValorProduto, null);
            var carrinho = _carrinhoGateway.GetValue("NumeroCliente", peditoRecebido.IdCliente);
            if (carrinho.Result == null)
                _pedidoCriarUseCase.CriarPedido(peditoRecebido.IdCliente, produtoCarrinho);
            else
                _pedidoAtualizarUseCase.AdicionarItemCarrinho(peditoRecebido.IdCliente, produtoCarrinho);
        }

        public void ProcessaPagamento(string mensagem)
        {
            var pagamentoRecebido = JsonSerializer.Deserialize<PagamentoDto>(mensagem);

            if (pagamentoRecebido != null)
                if (pagamentoRecebido.Status == EStatusPagamentoExtensions.ToDescriptionString(EStatusPagamento.Aprovado).ToString())
                    _pedidoAtualizarUseCase.AlterarStatusPedido(pagamentoRecebido.CodigoPedido, EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Pago).ToString());
                if (pagamentoRecebido.Status == EStatusPagamentoExtensions.ToDescriptionString(EStatusPagamento.Negado).ToString())
                    _pedidoAtualizarUseCase.AlterarStatusPedido(pagamentoRecebido.CodigoPedido, EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PagamentoNaoAprovado).ToString());
        }
    }
}
