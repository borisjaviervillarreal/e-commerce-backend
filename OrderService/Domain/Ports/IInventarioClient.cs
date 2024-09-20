namespace OrderService.Domain.Ports
{
    public interface IInventarioClient
    {
        Task<bool> VerificarStock(int productoId, int cantidad);
    }
}
