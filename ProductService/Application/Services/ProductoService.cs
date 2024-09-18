using AutoMapper;
using ProductService.Application.DTOs;
using ProductService.Domain.Entities;
using ProductService.Domain.Ports;

namespace ProductService.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductoDto> CreateProductoAsync(ProductoDto productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _unitOfWork.ProductoRepository.CreateAsync(producto);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ProductoDto>(producto);
        }

        public async Task<ProductoDto> GetByIdAsync(int id)
        {
            var producto = await _unitOfWork.ProductoRepository.GetByIdAsync(id);
            return _mapper.Map<ProductoDto>(producto);
        }

        public async Task<IEnumerable<ProductoDto>> GetAllAsync()
        {
            var productos = await _unitOfWork.ProductoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductoDto>>(productos);
        }

        public async Task UpdateProductoAsync(ProductoDto productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            await _unitOfWork.ProductoRepository.UpdateAsync(producto);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductoAsync(int id)
        {
            await _unitOfWork.ProductoRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }

}
