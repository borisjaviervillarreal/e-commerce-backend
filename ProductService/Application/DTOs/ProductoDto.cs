using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Application.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }  // Id del producto
        public string Nombre { get; set; } = string.Empty;  // Nombre del producto
        public string Descripcion { get; set; } = string.Empty;  // Descripción del producto
        public decimal Precio { get; set; }  // Precio del producto
        public int Stock { get; set; }  // Cantidad disponible en inventario
    }
}
