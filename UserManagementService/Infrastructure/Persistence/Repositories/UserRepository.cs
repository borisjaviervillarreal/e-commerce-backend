using Microsoft.AspNetCore.Identity;
using UserManagementService.Domain.Entities;
using UserManagementService.Domain.Ports;
using UserManagementService.Infrastructure.Data;

namespace UserManagementService.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Inyectamos el UserManager y el ApplicationDbContext en el repositorio
        public UserRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Buscar un usuario por email
        public async Task<User> FindByEmailAsync(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser != null)
            {
                return new User
                {
                    Id = identityUser.Id,
                    UserName = identityUser.UserName,
                    Email = identityUser.Email
                };
            }
            return null;
        }

        // Agregar un nuevo usuario utilizando UserManager de Identity
        public async Task AddUserAsync(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };
            await _userManager.CreateAsync(identityUser);
        }
    }
}
