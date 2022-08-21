using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Models.ProductModels.ViewModels;
using Work_with_orders.Repositories;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<IActionResult> GetProducts()
    {
        var products = _productRepository.GetAll();

        var productsViewModels = new List<ProductViewModel>();

        foreach (var product in products)
        {
            var productViewModel = new ProductViewModel();
            _mapper.Map(product, productViewModel);
            productsViewModels.Add(productViewModel);
        }

        return new OkObjectResult(productsViewModels);
    }

    public async Task<IActionResult> GetProductById(long id)
    {
        var product = await _productRepository.GetById(id);
        
        if (Equals(product, null))
        {
            return new BadRequestObjectResult("The product does not exist.");
        }
        
        var productViewModel = new ProductViewModel();
        _mapper.Map(product, productViewModel);

        return new OkObjectResult(productViewModel);
    }

    public async Task<IActionResult> CreateProduct(CreateProductRequestModel request)
    {
        var product = new Entities.Product();
        _mapper.Map(request, product);

        await _productRepository.Add(product);
        await _productRepository.Save();

        var response = new CreateProductResponseModel()
        {
            Message = "The product was created successfully.",
        };

        return new OkObjectResult(response);
    }

    public async Task<IActionResult> UpdateProduct(UpdateProductRequestModel request)
    {
        var product = await _productRepository.GetById(request.Id);

        if (Equals(product, null))
        {
            return new BadRequestObjectResult("The product does not exist.");
        }

        _mapper.Map(request, product);
        _productRepository.Update(product);
        await _productRepository.Save();

        var response = new UpdateProductResponseModel()
        {
            Message = "The product updated successfully."
        };

        return new OkObjectResult(response);
    }

    public async Task<IActionResult> DeleteProductById(long id)
    {
        var product = await _productRepository.GetById(id);

        if (Equals(product, null))
        {
            return new BadRequestObjectResult("The product does not exist.");
        }

        _productRepository.Delete(product);
        await _productRepository.Save();

        return new OkObjectResult("The product was successfully deleted.");
    }

    public async Task<IActionResult> AddProductQuantity(
        AddProductQuantityRequestModel request)
    {
        var product = await _productRepository.GetById(request.Id);
        
        if (Equals(product, null))
        {
            return new BadRequestObjectResult("The product does not exist.");
        }

        product.Quantity += request.Quantity;
        _productRepository.Update(product);
        await _productRepository.Save();

        var response = new AddProductQuantityResponseModel()
        {
            Message = "The product was added successfully."
        };

        return new OkObjectResult(response);
    }

    private static object _lock = new object();

    public async Task<IActionResult> RemoveProductQuantity(
        RemoveProductQuantityRequestModel request)
    {
        var isTaken = _productRepository.TakeProduct(request.Id, request.Quantity);

        if (isTaken)
        {
            var response = new RemoveProductQuantityResponseModel()
            {
                Message = "The product was successfully deleted."
            };

            return new OkObjectResult(response);
        }

        return new BadRequestObjectResult("So many products are not in warehouse.");
    }
}