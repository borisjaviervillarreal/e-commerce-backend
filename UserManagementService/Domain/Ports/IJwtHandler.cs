namespace UserManagementService.Domain.Ports
{
    // Interfaz para manejar JWT
    public interface IJwtHandler
    {
        Task<string> GenerateJwtToken(string email);
    }
}
