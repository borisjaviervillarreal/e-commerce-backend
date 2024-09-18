using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Services;
using ProductService.Domain.Ports;

namespace ProductService.Adapters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] ProductoDto productoDto)
        {
            var producto = await _productoService.CreateProductoAsync(productoDto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoDto productoDto)
        {
            if (id != productoDto.Id)
            {
                return BadRequest();
            }

            await _productoService.UpdateProductoAsync(productoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            await _productoService.DeleteProductoAsync(id);
            return NoContent();
        }
    }

}
