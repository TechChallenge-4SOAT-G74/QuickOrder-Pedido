

using QuickOrderPedido.Application.Dtos.Base;
using QuickOrderPedido.Domain.Entities;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoAtualizarUseCase : IBaseUseCase
    {
        Task<ServiceResult> AlterarItemAoPedido(string id, List<ProdutoCarrinho> pedidoDto);
        Task<ServiceResult> AlterarStatusPedido(int id, string pedidoStatus);
        Task<ServiceResult> ConfirmarPedido(int id);
    }
}
