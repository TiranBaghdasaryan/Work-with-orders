using AutoMapper;
using Work_with_orders.Entities;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Models.ResponseModels;

namespace Work_with_orders.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<SignInRequestModel, User>();
        CreateMap<SignUpRequestModel, User>();

        CreateMap<CreateProductRequestModel, Product>();
        CreateMap<UpdateProductRequestModel, Product>();

        CreateMap<Product, ProductViewModel>();
        
        
        CreateMap<Order, OrderViewModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));;
    }
}