using Moq;
using QuickOrderPedido.Application.UseCases;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using Xunit;

namespace QuickOrderPedido.Tests
{
    public class PedidoExcluirUseCaseTests
    {
        private readonly PedidoExcluirUseCase _pedidoExcluirUseCase;
        private readonly Mock<ICarrinhoGateway> _carrinhoGatewayMock;
        private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
        private readonly Mock<IPedidoStatusGateway> _pedidoStatusGatewayMock;

        public PedidoExcluirUseCaseTests()
        {
            _carrinhoGatewayMock = new Mock<ICarrinhoGateway>();
            _pedidoGatewayMock = new Mock<IPedidoGateway>();
            _pedidoStatusGatewayMock = new Mock<IPedidoStatusGateway>();
            _pedidoExcluirUseCase = new PedidoExcluirUseCase(_carrinhoGatewayMock.Object, _pedidoStatusGatewayMock.Object, _pedidoGatewayMock.Object);
        }

        [Fact]
        public async Task CancelarPedido_WhenPedidoExists_ShouldDeletePedidoAndCarrinho()
        {
            // Arrange
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(123, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });

            var pedido = new Pedido(DateTime.Now, null, 1, "123", 10.0, true, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            _pedidoGatewayMock.Setup(g => g.Get(pedido.Id.ToString())).ReturnsAsync(pedido);
            _carrinhoGatewayMock.Setup(g => g.Get(pedido.CarrinhoId)).ReturnsAsync(carrinho);


            // Act
            var result = await _pedidoExcluirUseCase.CancelarPedido(pedido.Id.ToString(), "Pagamento não aprovado");

            // Assert
            Assert.True(result.IsSuccess);
            _pedidoGatewayMock.Verify(g => g.Delete(pedido.Id.ToString()), Times.Once);
            _carrinhoGatewayMock.Verify(g => g.Delete(carrinho.Id.ToString()), Times.Once);
        }

        [Fact]
        public async Task CancelarPedido_WhenPedidoDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var codigoPedido = "123";
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });

            var pedido = new Pedido(DateTime.Now, null, 1, "123", 10.0, true, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            _pedidoGatewayMock.Setup(g => g.Get(codigoPedido)).ReturnsAsync((Pedido)null);


            // Act
            var result = await _pedidoExcluirUseCase.CancelarPedido(codigoPedido, "Pagamento não aprovado");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Pedido não encontrado.", result.Errors[0].Message);
        }
    }
}
