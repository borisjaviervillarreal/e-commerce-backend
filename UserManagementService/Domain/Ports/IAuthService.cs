using UserManagementService.DTOs;

namespace UserManagementService.Domain.Ports
{
    // Interfaz para el servicio de autenticación
    public interface IAuthService
    {
        // Método para registrar un nuevo usuario
        Task<bool> RegisterUserAsync(UserDto userDto);

        // Método para generar un token JWT a partir de un usuario
        Task<string> GenerateJwtToken(UserDto userDto);
    }
}
