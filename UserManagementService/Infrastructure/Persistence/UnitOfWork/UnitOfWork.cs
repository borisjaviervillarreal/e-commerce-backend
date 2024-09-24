using UserManagementService.Domain.Ports;
using UserManagementService.Infrastructure.Data;

namespace UserManagementService.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository Users { get; }

        // Constructor que recibe el ApplicationDbContext y el repositorio de usuarios
        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            Users = userRepository;
        }

        // Confirmar cambios en la base de datos
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Dispose para liberar recursos del contexto
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
