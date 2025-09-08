using AutoMapper;
using ProductsControl.DTO;
using ProductsControl.Models;

namespace ProductsControl.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, AddProductDto>().ReverseMap();
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Order, AddOrderDto>().ReverseMap();
            CreateMap<Order, GetOrderDto>().ReverseMap();
            CreateMap<OrderItem, AddOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, GetOrderItemDto>().ReverseMap();
        }
    }
}
