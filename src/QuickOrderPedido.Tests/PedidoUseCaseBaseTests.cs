using Moq;
using QuickOrderPedido.Application.UseCases;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using Xunit;

namespace QuickOrderPedido.Tests
{
    public class PedidoUseCaseBaseTests
    {

        private readonly PedidoUseCaseBase _pedidoUseCaseBase;
        private readonly Mock<ICarrinhoGateway> _carrinhoGatewayMock;
        private readonly Mock<IPedidoStatusGateway> _pedidoStatusGatewayMock;

        public PedidoUseCaseBaseTests()
        {
            _carrinhoGatewayMock = new Mock<ICarrinhoGateway>();
            _pedidoStatusGatewayMock = new Mock<IPedidoStatusGateway>();
            _pedidoUseCaseBase = new PedidoUseCaseBase(_carrinhoGatewayMock.Object, _pedidoStatusGatewayMock.Object);
        }


        [Fact]
        public async Task AlterarStatusPedido_WhenPedidoExists_ShouldUpdatePedidoAndReturnSuccessResult()
        {
            // Arrange
            var codigoPedido = "123";
            var status = "PagamentoAprovado";
            var pedidoStatus = new PedidoStatus(codigoPedido, status, DateTime.Now);

            _pedidoStatusGatewayMock.Setup(g => g.GetValue("CodigoPedido", codigoPedido)).ReturnsAsync(pedidoStatus);

            // Act
            var result = await _pedidoUseCaseBase.AlterarStatusPedido(codigoPedido, status);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AlterarStatusPedido_WhenPedidoDoesNotExist_ShouldReturnErrorResult()
        {
            // Arrange
            var codigoPedido = "123";
            var status = "PagamentoAprovado";
            var pedidoStatus = new PedidoStatus(codigoPedido, status, DateTime.Now);

            _pedidoStatusGatewayMock.Setup(g => g.GetValue("CodigoPedido", codigoPedido)).ReturnsAsync(pedidoStatus);

            // Act
            var result = await _pedidoUseCaseBase.AlterarStatusPedido("321", status);


            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Pedido não localizado.", result.Errors[0].Message);
        }

        [Fact]
        public async Task LimparCarrinho_WhenPedidoExists_ShouldUpdatePedidoAndReturnSuccessResult()
        {
            // Arrange
            var numeroCliente = 123;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                 new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(numeroCliente, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });

            _carrinhoGatewayMock.Setup(g => g.GetValue("NumeroCliente", numeroCliente)).ReturnsAsync(carrinho);

            // Act
            var result = await _pedidoUseCaseBase.LimparCarrinho(numeroCliente);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task LimparCarrinho_WhenPedidoDoesNotExist_ShouldReturnErrorResult()
        {
            // Arrange
            var numeroCliente = 123;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                 new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(numeroCliente, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });

            _carrinhoGatewayMock.Setup(g => g.GetValue("NumeroCliente", numeroCliente)).ReturnsAsync(carrinho);

            // Act
            var result = await _pedidoUseCaseBase.LimparCarrinho(321);


            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
