using ApiGateway.Domain.Entities;
using ApiGateway.Domain.Ports;
using ApiGateway.DTOs;
using ApiGateway.Infrastructure.Security;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiGateway.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper; // Inyectamos AutoMapper

        // Inyectamos el Unit of Work, JwtHandler y el IMapper para AutoMapper
        public AuthService(IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        // Registrar un nuevo usuario usando AutoMapper para mapear de UserDto a User
        public async Task<bool> RegisterUserAsync(UserDto userDto)
        {
            // Usamos el IMapper para mapear de UserDto a la entidad User
            var user = _mapper.Map<User>(userDto);

            await _unitOfWork.Users.AddUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // Generar un JWT usando el JwtHandler
        public async Task<string> GenerateJwtToken(UserDto userDto)
        {
            return await _jwtHandler.GenerateJwtToken(userDto.Email);
        }
    }
}
