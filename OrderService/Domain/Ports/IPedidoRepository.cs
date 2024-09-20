using OrderService.Domain.Entities;

namespace OrderService.Domain.Ports
{
    public interface IPedidoRepository
    {
        Task<Pedido> CreateAsync(Pedido producto);
        Task<Pedido> GetByIdAsync(int id);
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task UpdateAsync(Pedido producto);
        Task DeleteAsync(int id);
    }
}
