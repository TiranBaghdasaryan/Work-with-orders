using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.BasketModels.AddProductInBasket;
using Work_with_orders.Models.ProductModels.ViewModels;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Authorize(Roles = "User")]
[Route("v1/basket")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<ProductInBasketViewModel>>> GetProductsInBasket()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var response = await _basketService.GetProductsInBasketByEmail(email);
        return response;
    }

    [HttpPost("products")]
    public async Task<ActionResult<AddProductInBasketResponseModel>> AddProductInBasket(
        AddProductInBasketRequestModel request)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var response = await _basketService.AddProductInBasket(request, email);
        return response;
    }

    [HttpDelete("products/{id}")]
    public async Task<IActionResult> RemoveProductFromBasket(long id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var response = await _basketService.RemoveProductFromBasket(id, email);
        return response;
    }

    [HttpDelete("products")]
    public async Task<IActionResult> RemoveAllProductsFromBasket()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var response = await _basketService.RemoveAllProductsFromBasketByEmail(email);
        return response;
    }
}