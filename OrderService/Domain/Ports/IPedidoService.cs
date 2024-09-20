using OrderService.Application.DTOs;

namespace OrderService.Domain.Ports
{
    public interface IPedidoService
    {
        Task<PedidoDto> CreatePedidoAsync(PedidoDto pedidoDto);
        Task<PedidoDto> GetByIdAsync(int id);
        Task<IEnumerable<PedidoDto>> GetAllAsync();
        Task UpdatePedidoAsync(PedidoDto productoDto);
        Task DeletePedidoAsync(int id);

    }
}
