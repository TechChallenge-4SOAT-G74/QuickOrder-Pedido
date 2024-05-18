using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoCriarUseCase
    {
        Task<ServiceResult> CriarPedido(int numeroCliente, ProdutoCarrinho? produtoCarrinho = null);
    }
}
