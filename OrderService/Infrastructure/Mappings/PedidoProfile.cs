using AutoMapper;
using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
using static OrderService.Domain.Entities.Pedido;

namespace OrderService.Infrastructure.Mappings
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            // Mapeo entre Pedido y PedidoDto
            CreateMap<Pedido, PedidoDto>()
                .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Productos))
                .ReverseMap();

            // Mapeo entre ProductoPedido y ProductoPedidoDto
            CreateMap<ProductoPedido, ProductoPedidoDto>()
                .ReverseMap();
        }

    }
}
