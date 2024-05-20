using QuickOrderPedido.Application.Dtos.Base;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoExcluirUseCase
    {
        Task<ServiceResult> CancelarPedido(string codigoPedido, string statusPedido);
    }
}
