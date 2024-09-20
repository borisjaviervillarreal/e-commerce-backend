using OrderService.Domain.Ports;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Persistence.Repositories;

namespace OrderService.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private PedidoRepository _pedidoRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IPedidoRepository PedidoRepository 
        { 
            get { return _pedidoRepository ??= new PedidoRepository(_context); } 
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
