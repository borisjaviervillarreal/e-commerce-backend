using ApiGateway.Application.Services;
using ApiGateway.Domain.Entities;
using ApiGateway.Domain.Ports;
using ApiGateway.DTOs;
using ApiGateway.Infrastructure.Security;
using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IJwtHandler> _jwtHandlerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            // Configuramos los mocks para las dependencias de AuthService
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _jwtHandlerMock = new Mock<IJwtHandler>(); // Mock de la interfaz IJwtHandler
            _mapperMock = new Mock<IMapper>();

            // Creamos la instancia de AuthService con los mocks
            _authService = new AuthService(_unitOfWorkMock.Object, _jwtHandlerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnTrue_WhenUserIsSuccessfullyRegistered()
        {
            // Arrange
            var userDto = new UserDto { UserName = "testuser", Email = "test@example.com", Password = "Password123" };
            var user = new User { UserName = "testuser", Email = "test@example.com" };

            _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(user);
            _unitOfWorkMock.Setup(u => u.Users.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _authService.RegisterUserAsync(userDto);

            // Assert
            result.Should().BeTrue();
            _unitOfWorkMock.Verify(u => u.Users.AddUserAsync(It.IsAny<User>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public void GenerateJwtToken_ShouldReturnToken_WhenCalled()
        {
            // Arrange
            var user = new User { Email = "test@example.com" };
            var expectedToken = "test-jwt-token";

            _jwtHandlerMock.Setup(j => j.GenerateJwtToken(user.Email)).Returns(expectedToken);

            // Act
            var token = _authService.GenerateJwtToken(user);

            // Assert
            token.Should().Be(expectedToken);
            _jwtHandlerMock.Verify(j => j.GenerateJwtToken(user.Email), Times.Once);
        }
    }
}
