using AutoMapper;
using Work_with_orders.Entities;
using Work_with_orders.Models.AuthenticationModels.SignIn;
using Work_with_orders.Models.AuthenticationModels.SignUp;
using Work_with_orders.Models.Product;

namespace Work_with_orders.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<SignInRequestModel, User>();
        CreateMap<SignUpRequestModel, User>();

        CreateMap<ProductCreateModel, Product>();
        CreateMap<ProductUpdateModel, Product>();

        CreateMap<Product, ProductViewModel>();
    }
}