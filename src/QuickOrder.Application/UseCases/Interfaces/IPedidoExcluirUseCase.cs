using QuickOrderPedido.Application.Dtos.Base;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoExcluirUseCase : IBaseUseCase
    {
        Task<ServiceResult> CancelarPedido(string carrinhoId);
    }
}
