using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            try
            {
                if (pedido == null)
                {
                    throw new ArgumentNullException(nameof(pedido), "El pedido no puede ser nulo.");
                }

                await _context.Pedidos.AddAsync(pedido);
                return pedido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }

        public async Task DeleteAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if(pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
        }
    }
}
