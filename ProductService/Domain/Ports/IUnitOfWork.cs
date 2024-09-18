namespace ProductService.Domain.Ports
{
    public interface IUnitOfWork : IDisposable
    {
        IProductoRepository ProductoRepository { get; }
        Task<int> CompleteAsync();
    }
}
