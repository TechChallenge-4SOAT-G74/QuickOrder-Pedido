using Moq;
using QuickOrderPedido.Application.UseCases;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Infra.MQ;
using Xunit;

namespace QuickOrderPedido.Tests
{
    public class PedidoAtualizarUseCaseTests
    {
        private readonly PedidoAtualizarUseCase _pedidoAtualizarUseCase;
        private readonly Mock<ICarrinhoGateway> _carrinhoGatewayMock;
        private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
        private readonly Mock<IPedidoStatusGateway> _pedidoStatusGatewayMock;
        private readonly Mock<IRabbitMqPub<Pedido>> _rabbitMqPubMock;

        public PedidoAtualizarUseCaseTests()
        {
            _carrinhoGatewayMock = new Mock<ICarrinhoGateway>();
            _pedidoGatewayMock = new Mock<IPedidoGateway>();
            _pedidoStatusGatewayMock = new Mock<IPedidoStatusGateway>();
            _rabbitMqPubMock = new Mock<IRabbitMqPub<Pedido>>();
            _pedidoAtualizarUseCase = new PedidoAtualizarUseCase(_carrinhoGatewayMock.Object, _pedidoStatusGatewayMock.Object, _pedidoGatewayMock.Object, _rabbitMqPubMock.Object);
        }

        [Fact]
        public async Task AdicionarItemCarrinho_Should_AddItemToCarrinho()
        {
            // Arrange
            var numeroCliente = 123;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
               new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(numeroCliente, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });

            _carrinhoGatewayMock.Setup(g => g.GetValue("NumeroCliente", numeroCliente)).ReturnsAsync(carrinho);

            // Act
            var result = await _pedidoAtualizarUseCase.AdicionarItemCarrinho(numeroCliente, produtoCarrinho);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AdicionarItemCarrinho_Should_ReturnError_WhenCarrinhoNotFound()
        {
            // Arrange
            var numeroCliente = 321;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(123, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });

            _carrinhoGatewayMock.Setup(g => g.GetValue("NumeroCliente", 123)).ReturnsAsync(carrinho);

            // Act
            var result = await _pedidoAtualizarUseCase.AdicionarItemCarrinho(numeroCliente, produtoCarrinho);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Sacola não localizado.", result.Errors[0].Message);
        }


        [Fact]
        public async Task RemoverItemCarrinho_Should_Remove_Product_From_Carrinho()
        {
            // Arrange
            var numeroCliente = 123;
            var produtoCarrinho1 = new ProdutoCarrinho("Lanche", "Produto 1", 1, 1, 10.0,
               new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var produtoCarrinho2 = new ProdutoCarrinho("Lanche", "Produto 2", 1, 1, 10.0,
               new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(numeroCliente, 20.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho1, produtoCarrinho2 });

            _carrinhoGatewayMock.Setup(g => g.GetValue("NumeroCliente", numeroCliente)).ReturnsAsync(carrinho);

            // Act
            var result = await _pedidoAtualizarUseCase.RemoverItemCarrinho(numeroCliente, produtoCarrinho1);

            // Assert
            Assert.Equal(1, carrinho.ProdutosCarrinho.Count());
            Assert.Equal(10, carrinho.Valor);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task RemoverItemCarrinho_Should_Return_Error_When_Carrinho_Not_Found()
        {
            // Arrange
            var numeroCliente = 321;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var carrinho = new Carrinho(123, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });

            _carrinhoGatewayMock.Setup(g => g.GetValue("NumeroCliente", 123)).ReturnsAsync(carrinho);

            // Act
            var result = await _pedidoAtualizarUseCase.RemoverItemCarrinho(numeroCliente, produtoCarrinho);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Single(result.Errors);
            Assert.Equal("Sacola não localizado.", result.Errors[0].Message);
        }


        [Fact]
        public async Task ConfirmarPagamentoPedido_WhenPedidoExistsAndStatusIsPago_ShouldUpdatePedidoAndReturnSuccessResult()
        {
            // Arrange
            var codigoPedido = "123";
            var pedidoStatus = "Pagamento Aprovado";
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var pedido = new Pedido(DateTime.Now, null, 1, "123", 10.0, true, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            _pedidoGatewayMock.Setup(g => g.GetValue("CodigoPedido", codigoPedido)).ReturnsAsync(pedido);

            // Act
            var result = await _pedidoAtualizarUseCase.ConfirmarPagamentoPedido(codigoPedido, pedidoStatus);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(pedido.PedidoPago);
            _pedidoGatewayMock.Verify(g => g.Update(pedido), Times.Once);
            _pedidoGatewayMock.Verify(g => g.Delete(pedido.Id.ToString()), Times.Never);
        }

        [Fact]
        public async Task ConfirmarPagamentoPedido_WhenPedidoDoesNotExist_ShouldReturnErrorResult()
        {
            // Arrange
            var codigoPedido = "123";
            var pedidoStatus = "Pago";
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var pedido = new Pedido(DateTime.Now, null, 1, "321", 10.0, false, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            _pedidoGatewayMock.Setup(g => g.GetValue("CodigoPedido", 321)).ReturnsAsync(pedido);

            // Act
            var result = await _pedidoAtualizarUseCase.ConfirmarPagamentoPedido(codigoPedido, pedidoStatus);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Pedido não localizado.", result.Errors[0].Message);
            _pedidoGatewayMock.Verify(g => g.Update(It.IsAny<Pedido>()), Times.Never);
            _pedidoGatewayMock.Verify(g => g.Delete(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task FinalizarCarrinho_WithValidNumeroCliente_ShouldUpdatePedidoAndChangeStatus()
        {
            // Arrange
            int numeroCliente = 123;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
               new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });

            var carrinho = new Carrinho(numeroCliente, 10.0, DateTime.Now, new List<ProdutoCarrinho> { produtoCarrinho });

            var pedido = new Pedido(DateTime.Now, null, 1, "123", 10.0, false, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            _carrinhoGatewayMock.Setup(x => x.GetValue("NumeroCliente", numeroCliente)).ReturnsAsync(carrinho);
            _pedidoGatewayMock.Setup(x => x.GetValue("CarrinhoId", carrinho.Id.ToString())).ReturnsAsync(pedido);

            // Act
            var result = await _pedidoAtualizarUseCase.FinalizarCarrinho(numeroCliente);

            // Assert
            Assert.Empty(result.Errors);
            Assert.Equal(carrinho.ProdutosCarrinho, pedido.Produtos);
            Assert.Equal(carrinho.Valor, pedido.ValorPedido);
            Assert.False(pedido.PedidoPago);
            _pedidoGatewayMock.Verify(x => x.Update(pedido), Times.Once);
            //_pedidoGatewayMock.Verify(x => x.AlterarStatusPedido(pedido.Id.ToString(), EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PendentePagamento)), Times.Once);
        }

        [Fact]
        public async Task FinalizarCarrinho_WithInvalidNumeroCliente_ShouldReturnError()
        {
            // Arrange
            var numeroCliente = 321;
            var produtoCarrinho = new ProdutoCarrinho("Lanche", "Produto 1", 1, 2, 10.0,
                new List<ProdutoItensCarrinho> { new ProdutoItensCarrinho { NomeProdutoItem = "Pão", Quantidade = 1, ValorItem = 10.0 } });
            var pedido = new Pedido(DateTime.Now, null, 1, "123", 10.0, false, new List<ProdutoCarrinho> { produtoCarrinho }, null);

            _pedidoGatewayMock.Setup(g => g.GetValue("NumeroCliente", 123)).ReturnsAsync(pedido);

            // Act
            var result = await _pedidoAtualizarUseCase.FinalizarCarrinho(numeroCliente);

            // Assert
            Assert.Single(result.Errors);
            Assert.Equal("Sacola não localizado.", result.Errors[0].Message);
            _pedidoGatewayMock.Verify(x => x.Update(It.IsAny<Pedido>()), Times.Never);
            //_pedidoGatewayMock.Verify(x => x.AlterarStatusPedido(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task FinalizarCarrinho_WithNullPedido_ShouldReturnError()
        {
            // Arrange
            int numeroCliente = 123;
            var carrinho = new Carrinho();

            _carrinhoGatewayMock.Setup(x => x.GetValue("NumeroCliente", numeroCliente)).ReturnsAsync(carrinho);

            // Act
            var result = await _pedidoAtualizarUseCase.FinalizarCarrinho(numeroCliente);

            // Assert
            Assert.Single(result.Errors);
            Assert.Equal("Pedido não localizado.", result.Errors[0].Message);
            _pedidoGatewayMock.Verify(x => x.Update(It.IsAny<Pedido>()), Times.Never);
            //_pedidoGatewayMock.Verify(x => x.AlterarStatusPedido(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

    }
}
