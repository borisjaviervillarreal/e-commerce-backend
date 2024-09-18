namespace ApiGateway.Domain.Ports
{
    // Interfaz para manejar JWT
    public interface IJwtHandler
    {
        string GenerateJwtToken(string email);
    }
}
