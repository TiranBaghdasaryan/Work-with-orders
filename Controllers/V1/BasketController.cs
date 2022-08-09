using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Entities;
using Work_with_orders.Models.Basket;
using Work_with_orders.Models.Product;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.BasketProductRepo;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Authorize(Roles = "User")]
[Route("v1/basket")]
public class BasketController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly BasketRepository _basketRepository;
    private readonly BasketProductRepository _basketProductRepository;
    private readonly ProductRepository _productRepository;

    public BasketController
    (
        UserRepository userRepository,
        BasketRepository basketRepository,
        BasketProductRepository basketProductRepository,
        ProductRepository productRepository
    )
    {
        _userRepository = userRepository;
        _basketRepository = basketRepository;
        _basketProductRepository = basketProductRepository;
        _productRepository = productRepository;
    }
    
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<ProductInBasketViewModel>>> GetProductsInBasket()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = new Basket()
            {
                UserId = user.Id,
            };
            await _basketRepository.Add(basket);
            await _basketRepository.Save();
        }
        
        var basketProducts = await _basketProductRepository.GetAllProductsInBasket(basket.Id);

        var productInBasketViewModels = new List<ProductInBasketViewModel>();

        foreach (var item in basketProducts)
        {
            var product = await _productRepository.GetById(item.ProductId);
            string name = product.Name;
            int quantity = item.Quantity;
            productInBasketViewModels.Add(new ProductInBasketViewModel()
            {
                Name = name,
                Quantity = quantity,
            });
        }

        return Ok(productInBasketViewModels);
    }

    [HttpPost("products")]
    public async Task<IActionResult> AddProductInBasket(AddProductInBasketModel model)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = new Basket()
            {
                UserId = user.Id,
            };
            await _basketRepository.Add(basket);
            await _basketRepository.Save();
        }

        try
        {
            await _basketProductRepository.AddProductInBasket(basket.Id, model.ProductId, model.Quantity);
            await _basketProductRepository.Save();
            return Ok("The product was successfully added to the basket.");
        }
        catch
        {
            return BadRequest("The product is already in the basket.");
        }
    }

    [HttpDelete("products/{productId}")]
    public async Task<IActionResult> RemoveProductFromBasket(long productId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = new Basket()
            {
                UserId = user.Id,
            };
            await _basketRepository.Add(basket);
            await _basketRepository.Save();
        }

        var isDeleted = await _basketProductRepository.RemoveProductFromBasket(basket.Id, productId);
        await _basketProductRepository.Save();
        if (!isDeleted)
        {
            return BadRequest("The product doesn't removed from basket.");
        }

        return Ok("The product has been removed from the basket successfully.");
    }

    [HttpDelete("products")]
    public async Task<IActionResult> RemoveAllProductsFromBasket()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = new Basket()
            {
                UserId = user.Id,
            };
            await _basketRepository.Add(basket);
            await _basketRepository.Save();
        }

        var isDeleted = _basketProductRepository.RemoveAllProductsFromBasket(basket.Id);
        await _basketProductRepository.Save();

        if (!isDeleted)
        {
            return BadRequest("The products doesn't removed from basket.");
        }

        return Ok("The product has been removed from the basket successfully.");
    }
    
    
}