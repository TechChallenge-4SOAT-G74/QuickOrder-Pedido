using QuickOrderPedido.Application.Dtos.Base;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoCriarUseCase : IBaseUseCase
    {
        Task<ServiceResult> CriarPedido(int? numeroCliente = null);
    }
}
