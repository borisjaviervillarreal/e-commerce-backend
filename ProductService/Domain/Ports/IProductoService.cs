using ProductService.Application.DTOs;

namespace ProductService.Domain.Ports
{
    public interface IProductoService
    {
        Task<ProductoDto> CreateProductoAsync(ProductoDto productoDto);
        Task<ProductoDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductoDto>> GetAllAsync();
        Task UpdateProductoAsync(ProductoDto productoDto);
        Task DeleteProductoAsync(int id);
    }
}
