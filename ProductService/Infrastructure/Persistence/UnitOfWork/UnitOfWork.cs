using ProductService.Domain.Ports;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Persistence.Repositories;

namespace ProductService.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ProductoRepository _productoRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IProductoRepository ProductoRepository
        {
            get { return _productoRepository ??= new ProductoRepository(_context); }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
