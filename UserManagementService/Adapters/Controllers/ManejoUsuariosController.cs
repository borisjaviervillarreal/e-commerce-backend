using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Domain.Ports;
using UserManagementService.DTOs;

namespace UserManagementService.Adapters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManejoUsuariosController : ControllerBase
    {
        private readonly IAuthService _authService;
        public ManejoUsuariosController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] UserDto userDto)
        {
            var usuario = await _authService.RegisterUserAsync(userDto);

            return Ok(usuario);
        }

        [HttpPost("Autenticacion")]
        public async Task<IActionResult> Autenticacion([FromBody] UserDto userDto)
        {
            var token = await _authService.GenerateJwtToken(userDto);

            return Ok(token);
        }
    }
}
