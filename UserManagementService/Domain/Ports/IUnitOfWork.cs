namespace UserManagementService.Domain.Ports
{
    // Interfaz del Unit of Work para manejar transacciones y persistencia
    public interface IUnitOfWork : IDisposable
    {
        // Exponemos el repositorio de usuarios
        IUserRepository Users { get; }

        // Método para confirmar los cambios en la base de datos
        Task<int> CompleteAsync();
    }
}
