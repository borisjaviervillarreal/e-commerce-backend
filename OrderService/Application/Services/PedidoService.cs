using AutoMapper;
using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports;

namespace OrderService.Application.Services
{
    public class PedidoService :IPedidoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IInventarioClient _inventarioClient;

        public PedidoService(IUnitOfWork unitOfWork, IMapper mapper, IInventarioClient inventarioClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _inventarioClient = inventarioClient;
        }

        public async Task<PedidoDto> CreatePedidoAsync(PedidoDto pedidoDto)
        {

            try
            {
                // Verificar stock para cada producto antes de crear el pedido
                foreach (var productoDto in pedidoDto.Productos)
                {
                    bool stockDisponible = await _inventarioClient.VerificarStock(productoDto.ProductoId, productoDto.Cantidad);

                    if (!stockDisponible)
                    {
                        throw new Exception($"No hay suficiente stock para el producto {productoDto.Nombre}");
                    }
                }

                // Mapear el PedidoDto a la entidad Pedido
                var pedido = _mapper.Map<Pedido>(pedidoDto);
                // Guardar el pedido en la base de datos
                await _unitOfWork.PedidoRepository.CreateAsync(pedido);
                await _unitOfWork.CompleteAsync();

                // Devolver el PedidoDto creado
                return _mapper.Map<PedidoDto>(pedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<PedidoDto> GetByIdAsync(int id)
        {
            var pedido = await _unitOfWork.PedidoRepository.GetByIdAsync(id);
            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task<IEnumerable<PedidoDto>> GetAllAsync()
        {
            var pedidos = await _unitOfWork.PedidoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);    
        }

        public async Task UpdatePedidoAsync(PedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            await _unitOfWork.PedidoRepository.UpdateAsync(pedido);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePedidoAsync(int id)
        {
            await _unitOfWork.PedidoRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();  
        }
    }
}
