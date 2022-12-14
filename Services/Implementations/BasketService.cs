using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Models.ResponseModels;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.Implementations;
using Work_with_orders.Repositories.Interfaces;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Services.Implementations;

public class BasketService : IBasketService
{
    private readonly IUserRepository _userRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IBasketProductRepository _basketProductRepository;
    private readonly IProductRepository _productRepository;

    public BasketService(IUserRepository userRepository, IBasketRepository basketRepository,
        IBasketProductRepository basketProductRepository, IProductRepository productRepository)
    {
        _userRepository = userRepository;
        _basketRepository = basketRepository;
        _basketProductRepository = basketProductRepository;
        _productRepository = productRepository;
    }

    public async Task<ActionResult<IEnumerable<ProductInBasketViewModel>>> GetProductsInBasketByEmail(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = await CreateBasketByUserId(user.Id);
        }

        var basketProducts = await _basketProductRepository.GetAllProductsInBasketByBasketId(basket.Id);

        var productInBasketViewModels = new List<ProductInBasketViewModel>();

        foreach (var item in basketProducts)
        {
            var product = await _productRepository.GetById(item.ProductId);

            productInBasketViewModels.Add(new ProductInBasketViewModel()
            {
                Id = item.ProductId,
                Name = product.Name,
                Amount = product.Price * item.Quantity,
                Quantity = item.Quantity,
            });
        }

        return productInBasketViewModels;
    }

    public async Task<ActionResult<AddProductInBasketResponseModel>> AddProductInBasket(
        AddProductInBasketRequestModel request, string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = await CreateBasketByUserId(user.Id);
        }

        try
        {
            await _basketProductRepository.AddProductInBasket(basket.Id, request.ProductId, request.Quantity);
            await _basketProductRepository.Save();

            var response = new AddProductInBasketResponseModel
            {
                Message = "The product was successfully added to the basket."
            };

            return response;
        }
        catch
        {
            return new BadRequestObjectResult("The product is already in the basket.");
        }
    }

    public async Task<IActionResult> RemoveProductFromBasket(long id, string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = await CreateBasketByUserId(user.Id);
        }

        var isDeleted = await _basketProductRepository.RemoveProductFromBasket(basket.Id, id);
        await _basketProductRepository.Save();

        if (!isDeleted)
        {
            return new BadRequestObjectResult("The product doesn't removed from basket.");
        }

        return new OkObjectResult("The product has been removed from the basket successfully.");
    }

    public async Task<IActionResult> RemoveAllProductsFromBasketByEmail(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (Equals(basket, null))
        {
            basket = await CreateBasketByUserId(user.Id);
        }

        var isDeleted = _basketProductRepository.RemoveAllProductsFromBasket(basket.Id);
        await _basketProductRepository.Save();

        if (!isDeleted)
        {
            return new BadRequestObjectResult("The products doesn't removed from basket.");
        }

        return new OkObjectResult("The product has been removed from the basket successfully.");
    }


    private async Task<Entities.Basket> CreateBasketByUserId(long id)
    {
        var basket = new Entities.Basket()
        {
            UserId = id,
        };

        await _basketRepository.Add(basket);
        await _basketRepository.Save();

        return basket;
    }
}