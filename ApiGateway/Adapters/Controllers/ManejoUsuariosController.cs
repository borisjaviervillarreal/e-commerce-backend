using ApiGateway.Domain.Ports;
using ApiGateway.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Adapters.Controllers
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

        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] UserDto userDto)
        {
            var usuario = await _authService.RegisterUserAsync(userDto);

            return Ok(usuario);
        }

        [HttpPost]
        [Route("Autenticacion")]
        public async Task<IActionResult> Autenticacion([FromBody] UserDto userDto)
        {
            var token = await _authService.GenerateJwtToken(userDto);

            return Ok(token);
        }


    }
}
