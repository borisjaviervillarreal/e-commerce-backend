using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }   // ID del producto
        public string Nombre { get; set; } = string.Empty;   // Nombre del producto
        public string Descripcion { get; set; } = string.Empty;   // Descripción del producto
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }   // Precio del producto
        public int Stock { get; set; }   // Cantidad disponible en inventario
    }
}
