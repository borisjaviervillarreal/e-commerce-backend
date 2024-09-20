using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string EstadoPedido { get; set; } // Ejemplo: "Pendiente", "Confirmado"

        public List<ProductoPedido> Productos { get; set; } = new List<ProductoPedido>(); // Lista de productos en el pedido

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; } // Precio total del pedido

        public Pedido() { }

        public Pedido(DateTime fechaCreacion, string estadoPedido, List<ProductoPedido> productos, decimal total)
        {
            FechaCreacion = fechaCreacion;
            EstadoPedido = estadoPedido;
            Productos = productos;
            Total = total;
        }

        // Esta clase representa la relación entre Pedido y Producto
        public class ProductoPedido
        {
            public int ProductoId { get; set; }
            public string Nombre { get; set; }
            public int Cantidad { get; set; }

            [Column(TypeName = "decimal(18, 2)")]
            public decimal Precio { get; set; }

            // Subtotal para cada producto en el pedido
            [Column(TypeName = "decimal(18, 2)")]
            public decimal Subtotal => Cantidad * Precio;

            // Relación con Pedido
            public int PedidoId { get; set; } // Foreign Key para Pedido
        }
    }

}
