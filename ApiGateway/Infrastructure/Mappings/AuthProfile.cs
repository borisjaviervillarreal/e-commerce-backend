using ApiGateway.Domain.Entities;
using ApiGateway.DTOs;
using AutoMapper;

namespace ApiGateway.Infrastructure.Mappings
{
    // Definimos el perfil de AutoMapper para mapear entre User y UserDto
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // Configuración para mapear entre UserDto y User
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<User, UserDto>();
        }
    }
}
