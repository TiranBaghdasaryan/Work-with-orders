using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.BasketModels.AddProductInBasket;
using Work_with_orders.Models.ProductModels.ViewModels;

namespace Work_with_orders.Services.Basket;

public interface IBasketService
{
    Task<ActionResult<IEnumerable<ProductInBasketViewModel>>> GetProductsInBasketByEmail(string email);
    Task<ActionResult<AddProductInBasketResponseModel>> AddProductInBasket(AddProductInBasketRequestModel request,
        string email);
    Task<IActionResult> RemoveProductFromBasket(long id,string email);
    Task<IActionResult> RemoveAllProductsFromBasketByEmail(string email);
}