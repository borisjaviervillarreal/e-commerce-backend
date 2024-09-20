namespace OrderService.Domain.Ports
{
    public interface IUnitOfWork : IDisposable
    {
        IPedidoRepository PedidoRepository { get; }
        Task<int> CompleteAsync();
    }
}
