using QuickOrderPedido.Application.Dtos;
using QuickOrderPedido.Application.Dtos.Base;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoObterUseCase
    {
        Task<ServiceResult<PedidoDto>> ConsultarPedido(string id);
        Task<ServiceResult<List<PedidoDto>>> ConsultarListaPedidos();
    }
}
