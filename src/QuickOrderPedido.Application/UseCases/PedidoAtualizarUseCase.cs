
using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Application.Events;
using QuickOrderPedido.Application.UseCases.Interfaces;
using QuickOrderPedido.Domain.Adapters;
using QuickOrderPedido.Domain.Entities;
using QuickOrderPedido.Domain.Enums;

namespace QuickOrderPedido.Application.UseCases
{
    public class PedidoAtualizarUseCase : PedidoUseCaseBase, IPedidoAtualizarUseCase
    {
        private readonly ICarrinhoGateway _carrinhoGateway;
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IRabbitMqPub<Pedido> _rabbitMqPub;


        public PedidoAtualizarUseCase(ICarrinhoGateway carrinhoGateway,
                IPedidoStatusGateway pedidoStatusGateway,
                IPedidoGateway pedidoGateway,
                IRabbitMqPub<Pedido> rabbitMqPub) : base(carrinhoGateway, pedidoStatusGateway)
        {
            _carrinhoGateway = carrinhoGateway;
            _pedidoGateway = pedidoGateway;
            _rabbitMqPub = rabbitMqPub;
        }

        public async Task<ServiceResult> AdicionarItemCarrinho(int numeroCliente, ProdutoCarrinho produtoCarrinho)
        {
            var result = new ServiceResult();
            try
            {
                var carrinho = numeroCliente > 0 ? await  _carrinhoGateway.GetValue("NumeroCliente", numeroCliente) : new Carrinho();


                if (carrinho == null)
                {
                    result.AddError("Sacola não localizado.");
                    return result;
                }

                carrinho.DataAtualizacao = DateTime.Now;
                carrinho.Valor = carrinho.Valor + (produtoCarrinho.ValorProduto * produtoCarrinho.Quantidade);
                carrinho.ProdutosCarrinho.Add(produtoCarrinho);

                _carrinhoGateway.Update(carrinho);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }
        public async Task<ServiceResult> RemoverItemCarrinho(int numeroCliente, ProdutoCarrinho produtoCarrinho)
        {
            var result = new ServiceResult();
            try
            {
                var carrinho = numeroCliente > 0 ? await _carrinhoGateway.GetValue("NumeroCliente", numeroCliente) : new Carrinho();

                if (carrinho == null)
                {
                    result.AddError("Sacola não localizado.");
                    return result;
                }

                carrinho.DataAtualizacao = DateTime.Now;
                carrinho.Valor = carrinho.Valor - (produtoCarrinho.ValorProduto * produtoCarrinho.Quantidade);
                carrinho.Valor = carrinho.Valor < 0 ? 0 : carrinho.Valor;
                try
                {
                    carrinho.ProdutosCarrinho.Remove(produtoCarrinho);

                    _carrinhoGateway.Update(carrinho);
                }
                catch
                {
                    result.AddError("Produto não localizado no carrinho");
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> ConfirmarPagamentoPedido(string codigoPedido, string pedidoStatus)
        {
            var result = new ServiceResult();
            try
            {
                var pedido = await _pedidoGateway.GetValue("CodigoPedido", codigoPedido);

                if (pedido == null)
                {
                    result.AddError("Pedido não localizado.");
                    return result;
                }

                pedido.PedidoPago = pedidoStatus == EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.Pago);

                if (pedido.PedidoPago)
                    _pedidoGateway.Update(pedido);
                else
                    _pedidoGateway.Delete(pedido.Id.ToString());

                await AlterarStatusPedido(codigoPedido, pedidoStatus);

                await LimparCarrinho(pedido.ClienteId);


            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> FinalizarCarrinho(int numeroCliente)
        {
            var result = new ServiceResult();
            try
            {

                var carrinho = numeroCliente > 0 ? await _carrinhoGateway.GetValue("NumeroCliente", numeroCliente) : new Carrinho();

                if (carrinho == null)
                {
                    result.AddError("Sacola não localizado.");
                    return result;
                }

                var pedido = await _pedidoGateway.GetValue("CarrinhoId", carrinho.Id.ToString());

                if (pedido == null)
                {
                    result.AddError("Pedido não localizado.");
                    return result;
                }

                pedido.DataHoraFinalizado = DateTime.Now;
                pedido.Produtos = carrinho.ProdutosCarrinho;
                pedido.ValorPedido = carrinho.Valor;
                pedido.PedidoPago = false;
                _pedidoGateway.Update(pedido);


                AlterarStatusPedido(pedido.Id.ToString(), EStatusPedidoExtensions.ToDescriptionString(EStatusPedido.PendentePagamento));

            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> ConfirmarPedido(string codigoPedido)
        {
            var result = new ServiceResult();
            try
            {
                var pedido = await _pedidoGateway.Get(codigoPedido);

                if (pedido == null)
                {
                    result.AddError("Pedido não localizado.");
                    return result;
                }

                _rabbitMqPub.Publicar(pedido, "Pedido", "Pedido_Confirmado");
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

    }
}
