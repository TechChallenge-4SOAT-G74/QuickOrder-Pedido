using Moq;
using QuickOrderPedido.Application.UseCases;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using Xunit;

namespace QuickOrderPedido.Tests
{
    public class PedidoCriarUseCaseTests
    {
        private readonly PedidoCriarUseCase _pedidoCriarUseCase;
        private readonly Mock<ICarrinhoGateway> _carrinhoGatewayMock;
        private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
        private readonly Mock<IPedidoStatusGateway> _pedidoStatusGatewayMock;

        public PedidoCriarUseCaseTests()
        {
            _carrinhoGatewayMock = new Mock<ICarrinhoGateway>();
            _pedidoGatewayMock = new Mock<IPedidoGateway>();
            _pedidoStatusGatewayMock = new Mock<IPedidoStatusGateway>();
            _pedidoCriarUseCase = new PedidoCriarUseCase(_carrinhoGatewayMock.Object, _pedidoStatusGatewayMock.Object, _pedidoGatewayMock.Object);
        }

        [Fact]
        public async Task CriarPedido_WithValidData_ReturnsSuccessResult()
        {
            // Arrange
            var numeroCliente = 1;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
               new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });

            var pedidos = new List<Pedido> { new Pedido(DateTime.Now, null, 1, "123", 10.0, false, new List<ProdutoCarrinho> { produtoCarrinho }, null) };


            _pedidoGatewayMock.Setup(g => g.GetAll()).ReturnsAsync(pedidos);

            // Act
            var result = await _pedidoCriarUseCase.CriarPedido(numeroCliente, produtoCarrinho);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task CriarPedido_WithInvalidData_ReturnsErrorResult()
        {
            // Arrange
            var numeroCliente = 123;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
               new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(numeroCliente, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });


            // Act
            var result = await _pedidoCriarUseCase.CriarPedido(numeroCliente, produtoCarrinho);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
        }
    }
}
