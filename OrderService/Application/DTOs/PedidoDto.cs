namespace OrderService.Application.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string EstadoPedido { get; set; }
        public List<ProductoPedidoDto> Productos { get; set; } = new List<ProductoPedidoDto>(); // Lista de productos
        public decimal Total { get; set; } // Precio total del pedido
    }

    public class ProductoPedidoDto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; } // Subtotal para cada producto
        public int PedidoId { get; set; } // Asignar el PedidoId aquí
    }
}
