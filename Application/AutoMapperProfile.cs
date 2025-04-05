using Application.DTOs.Auth;
using Application.DTOs.Customer;
using Application.DTOs.Order;
using Application.DTOs.Owner;
using Application.DTOs.Product;
using AutoMapper;
using Domain.Entities;

namespace Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TCustomer, CustomerResponseDto>(); 

            CreateMap<TProduct, ProductResponseDto>();

            CreateMap<TOrder, OrderResponseDto>();

            CreateMap<TOrderDetail, OrderDetailsResponseDto>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.Product.name));

            CreateMap<TOwner, AuthResponseDto>();
            
            CreateMap<TOwner, OwnerResponseDto>()
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.role.ToString()))
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.firstName + " " + src.lastName));
        }
        
    }
}
