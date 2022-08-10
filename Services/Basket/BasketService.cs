﻿using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.BasketModels.AddProductInBasket;
using Work_with_orders.Models.ProductModels.ViewModels;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.BasketProductRepo;

namespace Work_with_orders.Services.Basket;

public class BasketService : IBasketService
{
    private readonly UserRepository _userRepository;
    private readonly BasketRepository _basketRepository;
    private readonly BasketProductRepository _basketProductRepository;
    private readonly ProductRepository _productRepository;

    public BasketService(UserRepository userRepository, BasketRepository basketRepository,
        BasketProductRepository basketProductRepository, ProductRepository productRepository)
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
            basket = new Entities.Basket()
            {
                UserId = user.Id,
            };

            await _basketRepository.Add(basket);
            await _basketRepository.Save();
        }

        var basketProducts = await _basketProductRepository.GetAllProductsInBasketByBasketId(basket.Id);

        var productInBasketViewModels = new List<ProductInBasketViewModel>();

        foreach (var item in basketProducts)
        {
            var product = await _productRepository.GetById(item.ProductId);
            string name = product.Name;
            int quantity = item.Quantity;

            productInBasketViewModels.Add(new ProductInBasketViewModel()
            {
                Id = item.ProductId,
                Name = name,
                Quantity = quantity,
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
            basket = new Entities.Basket()
            {
                UserId = user.Id,
            };

            await _basketRepository.Add(basket);
            await _basketRepository.Save();
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
            basket = new Entities.Basket()
            {
                UserId = user.Id,
            };
            await _basketRepository.Add(basket);
            await _basketRepository.Save();
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
            basket = new Entities.Basket()
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
            return new BadRequestObjectResult("The products doesn't removed from basket.");
        }

        return new OkObjectResult("The product has been removed from the basket successfully.");
    }
}