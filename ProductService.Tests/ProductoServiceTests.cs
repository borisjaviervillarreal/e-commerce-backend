using AutoMapper;
using FluentAssertions;
using Moq;
using ProductService.Application.DTOs;
using ProductService.Application.Services;
using ProductService.Domain.Entities;
using ProductService.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests
{
    public class ProductoServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IProductoRepository> _productoRepositoryMock;  // Añadir mock del repositorio
        private readonly IProductoService _productoService;

        public ProductoServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _productoRepositoryMock = new Mock<IProductoRepository>();  // Instanciar el mock del repositorio

            // Configurar el UnitOfWork para devolver el mock del ProductoRepository
            _unitOfWorkMock.Setup(u => u.ProductoRepository).Returns(_productoRepositoryMock.Object);

            _productoService = new ProductoService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateProductoAsync_ShouldReturnProductoDto_WhenProductIsCreated()
        {
            // Arrange
            var productoDto = new ProductoDto { Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 };
            var producto = new Producto { Id = 1, Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 };

            _mapperMock.Setup(m => m.Map<Producto>(It.IsAny<ProductoDto>())).Returns(producto);
            _mapperMock.Setup(m => m.Map<ProductoDto>(It.IsAny<Producto>())).Returns(productoDto);
            _unitOfWorkMock.Setup(u => u.ProductoRepository.CreateAsync(It.IsAny<Producto>())).ReturnsAsync(producto);

            // Act
            var result = await _productoService.CreateProductoAsync(productoDto);

            // Assert
            result.Should().NotBeNull();
            result.Nombre.Should().Be(productoDto.Nombre);
            _unitOfWorkMock.Verify(u => u.ProductoRepository.CreateAsync(It.IsAny<Producto>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProductoDto_WhenProductExists()
        {
            // Arrange
            var productoId = 1;
            var producto = new Producto { Id = productoId, Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 };
            var productoDto = new ProductoDto { Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 };

            _unitOfWorkMock.Setup(u => u.ProductoRepository.GetByIdAsync(productoId)).ReturnsAsync(producto);
            _mapperMock.Setup(m => m.Map<ProductoDto>(producto)).Returns(productoDto);

            // Act
            var result = await _productoService.GetByIdAsync(productoId);

            // Assert
            result.Should().NotBeNull();
            result.Nombre.Should().Be(productoDto.Nombre);
            _unitOfWorkMock.Verify(u => u.ProductoRepository.GetByIdAsync(productoId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfProductoDtos_WhenProductsExist()
        {
            // Arrange
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 },
                new Producto { Id = 2, Nombre = "Producto 2", Descripcion = "Descripción 2", Precio = 200m, Stock = 30 }
            };

            var productosDto = new List<ProductoDto>
            {
                new ProductoDto { Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 },
                new ProductoDto { Nombre = "Producto 2", Descripcion = "Descripción 2", Precio = 200m, Stock = 30 }
            };

            _unitOfWorkMock.Setup(u => u.ProductoRepository.GetAllAsync()).ReturnsAsync(productos);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProductoDto>>(productos)).Returns(productosDto);

            // Act
            var result = await _productoService.GetAllAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            _unitOfWorkMock.Verify(u => u.ProductoRepository.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProductoAsync_ShouldUpdateProducto_WhenProductIsValid()
        {
            // Arrange
            var productoDto = new ProductoDto { Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 };
            var producto = new Producto { Id = 1, Nombre = "Producto 1", Descripcion = "Descripción 1", Precio = 100m, Stock = 50 };

            _mapperMock.Setup(m => m.Map<Producto>(productoDto)).Returns(producto);
            _unitOfWorkMock.Setup(u => u.ProductoRepository.UpdateAsync(It.IsAny<Producto>()));

            // Act
            await _productoService.UpdateProductoAsync(productoDto);

            // Assert
            _unitOfWorkMock.Verify(u => u.ProductoRepository.UpdateAsync(It.IsAny<Producto>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductoAsync_ShouldDeleteProducto_WhenProductExists()
        {
            // Arrange
            var productoId = 1;

            _unitOfWorkMock.Setup(u => u.ProductoRepository.DeleteAsync(productoId));

            // Act
            await _productoService.DeleteProductoAsync(productoId);

            // Assert
            _unitOfWorkMock.Verify(u => u.ProductoRepository.DeleteAsync(productoId), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }


    }
}
