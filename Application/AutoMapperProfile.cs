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
            CreateMap<CustomerCreateDto, TCustomer>();

            CreateMap<TCustomer, CustomerResponseDto>();
            
            CreateMap<CustomerUpdateDto, TCustomer>();
                //.ForMember(dest => dest.updatedAt, opt => opt.Ignore())
                //.ForMember(dest => dest.createdAt, opt => opt.Ignore());

            CreateMap<ProductCreateDto, TProduct>()
                .ForMember(dest => dest.createdAt, opt => opt.Ignore())
                .ForMember(dest => dest.updatedAt, opt => opt.Ignore());

            CreateMap<ProductUpdateDto, TProduct>()
                .ForMember(dest => dest.updatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.createdAt, opt => opt.Ignore());    

            CreateMap<TProduct, ProductResponseDto>();


            CreateMap<OrderCreateDto, TOrder>();
            CreateMap<OrderDetailCreateDto, TOrderDetail>();

            CreateMap<TOrder, OrderResponseDto>();

            CreateMap<TOrderDetail, OrderDetailsResponseDto>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.Product.name));

            CreateMap<AuthDto, TOwner>();

            CreateMap<TOwner, AuthResponseDto>();
            
            CreateMap<TOwner, OwnerProfile>()
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.role.ToString()))
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.firstName + " " + src.lastName));
        }
        
    }
}
