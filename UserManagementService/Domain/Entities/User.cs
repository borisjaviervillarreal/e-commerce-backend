namespace UserManagementService.Domain.Entities
{
    // Entidad User que representa un usuario en el sistema
    public class User
    {
        public string Id { get; set; } // El ID del usuario, generalmente generado por Identity
        public string UserName { get; set; } // Nombre de usuario
        public string Email { get; set; } // Email del usuario
        public string PasswordHash { get; set; } // Contraseña encriptada
    }
}
