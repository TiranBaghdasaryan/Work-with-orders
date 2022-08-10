using AutoMapper;
using Work_with_orders.Entities;
using Work_with_orders.Models.AuthenticationModels.SignIn;
using Work_with_orders.Models.AuthenticationModels.SignUp;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Models.ProductModels.ViewModels;

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
    }
}