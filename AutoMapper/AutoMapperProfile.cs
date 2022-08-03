using AutoMapper;
using Work_with_orders.Entities;
using Work_with_orders.Models.Authentication;
using Work_with_orders.Models.Product;

namespace Work_with_orders.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<SignInModel, User>();
        CreateMap<SignUpModel, User>();

        CreateMap<ProductCreateModel, Product>();
        CreateMap<ProductUpdateModel, Product>();
        
        CreateMap<Product, ProductViewModel>();
    }
}