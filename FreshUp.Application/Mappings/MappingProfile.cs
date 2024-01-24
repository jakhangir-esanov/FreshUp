using AutoMapper;

namespace FreshUp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryResultDto, Category>().ReverseMap();
        CreateMap<InventoryResultDto, Inventory>().ReverseMap();
        CreateMap<OrderResultDto, Order>().ReverseMap();
        CreateMap<OrderListResultDto, OrderList>().ReverseMap();
        CreateMap<ProductResultDto, Product>().ReverseMap();
        CreateMap<UserResultDto, User>().ReverseMap();
    }
}
