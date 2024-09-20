using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Domain.Ports;

namespace OrderService.Adapters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoDto pedidoDto)
        {
            var pedido = await _pedidoService.CreatePedidoAsync(pedidoDto);
            return CreatedAtAction(nameof(ObtenerPedido), new { id = pedido.Id }, pedido);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedido(int id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosPedidos()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            await _pedidoService.DeletePedidoAsync(id);
            return NoContent();
        }
    }
}
