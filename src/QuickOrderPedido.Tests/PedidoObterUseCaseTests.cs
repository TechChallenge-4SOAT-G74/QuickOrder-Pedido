using Moq;
using QuickOrderPedido.Application.UseCases;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using Xunit;

namespace QuickOrderPedido.Tests
{
    public class PedidoObterUseCaseTests
    {
        private readonly PedidoObterUseCase _pedidoObterUseCase;
        private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
        private readonly Mock<IPedidoStatusGateway> _pedidoStatusGatewayMock;

        public PedidoObterUseCaseTests()
        {
            _pedidoGatewayMock = new Mock<IPedidoGateway>();
            _pedidoStatusGatewayMock = new Mock<IPedidoStatusGateway>();
            _pedidoObterUseCase = new PedidoObterUseCase(_pedidoGatewayMock.Object, _pedidoStatusGatewayMock.Object);
        }

        [Fact]
        public async Task ConsultarPedido_WhenPedidoAndFilaExist_ReturnsPedidoDto()
        {
            // Arrange
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var pedido = new Pedido(DateTime.Now, null, 1, "123", 10.0, true, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            var fila = new PedidoStatus(pedido.Id.ToString(), "Em preparação", DateTime.Now);

            _pedidoGatewayMock.Setup(g => g.Get(pedido.Id.ToString())).ReturnsAsync(pedido);

            _pedidoStatusGatewayMock.Setup(g => g.GetValue("CodigoPedido", pedido.Id.ToString())).ReturnsAsync(fila);

            // Act
            var result = await _pedidoObterUseCase.ConsultarPedido(pedido.Id.ToString());

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(pedido.ClienteId, result.Data.NumeroCliente);
            Assert.Equal(pedido.Id.ToString(), result.Data.CodigoPedido);
            Assert.Equal(pedido.DataHoraInicio, result.Data.DataHoraInicio);
            Assert.Equal(pedido.DataHoraFinalizado, result.Data.DataHoraFinalizado);
            Assert.Equal(pedido.Observacao, result.Data.Observacao);
            Assert.Equal(pedido.PedidoPago, result.Data.PedidoPago);
            Assert.Equal(pedido.ValorPedido, result.Data.ValorPedido);
            Assert.Equal(pedido.CarrinhoId, result.Data.CarrinhoId);
            // Assert other properties

            _pedidoGatewayMock.Verify(g => g.Get(pedido.Id.ToString()), Times.Once);
            _pedidoStatusGatewayMock.Verify(g => g.GetValue("CodigoPedido", pedido.Id.ToString()), Times.Once);
        }

        [Fact]
        public async Task ConsultarPedido_WhenPedidoOrFilaDoesNotExist_ReturnsError()
        {
            // Arrange
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var pedido = new Pedido(DateTime.Now, null, 1, "123", 10.0, true, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            var fila = new PedidoStatus(pedido.Id.ToString(), "Em preparação", DateTime.Now);

            _pedidoGatewayMock.Setup(g => g.Get(pedido.Id.ToString())).ReturnsAsync(pedido);


            // Act
            var result = await _pedidoObterUseCase.ConsultarPedido("123");

            // Assert
            Assert.Null(result.Data);
            Assert.Contains("Pedido não localizado", result.Errors[0].Message);
        }
    }
}
