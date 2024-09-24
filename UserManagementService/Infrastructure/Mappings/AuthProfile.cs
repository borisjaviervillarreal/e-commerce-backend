using AutoMapper;
using UserManagementService.Domain.Entities;
using UserManagementService.DTOs;

namespace UserManagementService.Infrastructure.Mappings
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
