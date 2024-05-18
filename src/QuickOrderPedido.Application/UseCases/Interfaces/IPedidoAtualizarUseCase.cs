

using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoAtualizarUseCase
    {
        Task<ServiceResult> AdicionarItemCarrinho(int numeroCliente, ProdutoCarrinho produtoCarrinho);
        Task<ServiceResult> RemoverItemCarrinho(int numeroCliente, ProdutoCarrinho produtoCarrinho);
        Task<ServiceResult> AlterarStatusPedido(string codigoPedido, string pedidoStatus);
        Task<ServiceResult> ConfirmarPagamentoPedido(string codigoPedido, string pedidoStatus);
        Task<ServiceResult> FinalizarCarrinho(int numeroCliente);
        Task<ServiceResult> ConfirmarPedido(string codigoPedido);
    }
}
