using AutoMapper;
using QuickOrderPedido.Application.Dtos;
using QuickOrderPedido.Application.Events;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
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

        public ProcessaEvento(ICarrinhoGateway carrinhoGateway, IPedidoCriarUseCase pedidoCriarUseCase, IPedidoAtualizarUseCase pedidoAtualizarUseCase)
        {
            _carrinhoGateway = carrinhoGateway;
            _pedidoCriarUseCase = pedidoCriarUseCase;
            _pedidoAtualizarUseCase = pedidoAtualizarUseCase;
        }

        public void Processa(string mensagem)
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
    }
}
