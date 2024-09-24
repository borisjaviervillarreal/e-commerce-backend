using AutoMapper;
using Moq;
using OrderService.Application.DTOs;
using OrderService.Application.Services;
using OrderService.Domain.Entities;
using OrderService.Domain.Ports;
using OrderService.Infrastructure.Mappings;
using OrderService.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests
{
    public class PedidoServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IInventarioClient> _inventarioClientMock;
        private readonly IPedidoService _pedidoService;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public PedidoServiceTests()
        {
            // Inicializa los mocks
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _pedidoRepositoryMock = new Mock<IPedidoRepository>(); // Mock del PedidoRepository
            _mapperMock = new Mock<IMapper>();
            _inventarioClientMock = new Mock<IInventarioClient>();

            // Configurar el UnitOfWorkMock para devolver el mock del PedidoRepository
            _unitOfWorkMock.Setup(u => u.PedidoRepository).Returns(_pedidoRepositoryMock.Object);

            // Inicializa el PedidoService con los mocks
            _pedidoService = new PedidoService(_unitOfWorkMock.Object, _mapperMock.Object, _inventarioClientMock.Object, _rabbitMQProducer);

        }

        [Fact]
        public async Task CrearPedido_StockDisponible_CreaPedidoExitosamente()
        {
            // Arrange
            var pedidoDto = new PedidoDto
            {
                Id = 1, // Asignar un ID para el pedido
                FechaCreacion = DateTime.Now, // Asignar la fecha de creación
                EstadoPedido = "Pendiente", // Asignar un estado para el pedido
                Total = 200.0m, // Asignar el total del pedido
                Productos = new List<ProductoPedidoDto>
                {
                    new ProductoPedidoDto
                    {
                        ProductoId = 1,
                        Cantidad = 2,
                        Nombre = "Producto 1",
                        Precio = 100.0m, // Asignar precio
                        Subtotal = 200.0m, // Asignar el subtotal o calcularlo
                        PedidoId = 1 // Relacionar con Pedido
                    }
                }
            };

            _inventarioClientMock.Setup(x => x.VerificarStock(It.IsAny<int>(), It.IsAny<int>()))
                                 .ReturnsAsync(true);

            _mapperMock.Setup(x => x.Map<Pedido>(It.IsAny<PedidoDto>()))
                       .Returns(new Pedido());

            // Act
            var resultado = await _pedidoService.CreatePedidoAsync(pedidoDto);

            // Assert
            _unitOfWorkMock.Verify(x => x.PedidoRepository.CreateAsync(It.IsAny<Pedido>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
        }


        [Fact]
        public async Task CrearPedido_StockNoDisponible_LanzaExcepcion()
        {
            // Arrange
            var pedidoDto = new PedidoDto
            {
                Productos = new List<ProductoPedidoDto>
                {
                    new ProductoPedidoDto { ProductoId = 1, Cantidad = 2, Nombre = "Producto 1" }
                }
            };

            _inventarioClientMock.Setup(x => x.VerificarStock(It.IsAny<int>(), It.IsAny<int>()))
                                 .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _pedidoService.CreatePedidoAsync(pedidoDto));

            _unitOfWorkMock.Verify(x => x.PedidoRepository.CreateAsync(It.IsAny<Pedido>()), Times.Never);
        }
    }
}
