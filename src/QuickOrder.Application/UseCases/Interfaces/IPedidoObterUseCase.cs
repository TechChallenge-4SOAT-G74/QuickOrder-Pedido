using QuickOrderPedido.Application.Dtos;
using QuickOrderPedido.Application.Dtos.Base;

namespace QuickOrderPedido.Application.UseCases.Interfaces
{
    public interface IPedidoObterUseCase : IBaseUseCase
    {
        Task<ServiceResult<PedidoDto>> ConsultarPedido(int id);
        Task<ServiceResult<List<PedidoDto>>> ConsultarListaPedidos();
    }
}
