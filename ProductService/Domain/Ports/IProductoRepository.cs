using ProductService.Domain.Entities;

namespace ProductService.Domain.Ports
{
    public interface IProductoRepository
    {
        Task<Producto> CreateAsync(Producto producto);
        Task<Producto> GetByIdAsync(int id);
        Task<IEnumerable<Producto>> GetAllAsync();
        Task UpdateAsync(Producto producto);
        Task DeleteAsync(int id);
    }
}
