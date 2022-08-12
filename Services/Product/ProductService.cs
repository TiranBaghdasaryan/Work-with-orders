using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Work_with_orders.Commands.Executors;
using Work_with_orders.Context;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Models.ProductModels.ViewModels;
using Work_with_orders.Repositories;

namespace Work_with_orders.Services.Product;

public class ProductService : IProductService
{
    private readonly ProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ProductService(
        ProductRepository productRepository,
        IMapper mapper, ApplicationContext applicationContext,
        ApplicationContext context)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _context = context;
    }


    public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
    {
        var products = _productRepository.GetAll();

        var productsViewModels = new List<ProductViewModel>();

        foreach (var product in products)
        {
            var productViewModel = new ProductViewModel();
            _mapper.Map(product, productViewModel);
            productsViewModels.Add(productViewModel);
        }

        return productsViewModels;
    }

    public async Task<ActionResult<ProductViewModel>> GetProductById(long id)
    {

        var product = await _productRepository.GetById(id);

        // if (Equals(product, null))
        // {
        //     return new BadRequestObjectResult("The product does not exist.");
        // }

        var productViewModel = new ProductViewModel();
        _mapper.Map(product, productViewModel);

        return productViewModel;
    }

    public async Task<ActionResult<CreateProductResponseModel>> CreateProduct(CreateProductRequestModel request)
    {
        var product = new Entities.Product();
        _mapper.Map(request, product);

        await _productRepository.Add(product);
        await _productRepository.Save();

        var response = new CreateProductResponseModel()
        {
            Message = "The product was created successfully.",
        };

        return response;
    }

    public async Task<ActionResult<UpdateProductResponseModel>> UpdateProduct(UpdateProductRequestModel request)
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

        return response;
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

    public async Task<ActionResult<AddProductQuantityResponseModel>> AddProductQuantity(
        AddProductQuantityRequestModel request)
    {
        var product = await _productRepository.GetById(request.Id);

        if (Equals(product, null))
        {
            return new BadRequestObjectResult("The product does not exist.");
        }

        product.Quantity += request.Quantity;
        await _productRepository.Save();

        var response = new AddProductQuantityResponseModel()
        {
            Message = "The product was added successfully."
        };

        return response;
    }

    private static object _lock = new object();

    public async Task<ActionResult<RemoveProductQuantityResponseModel>> RemoveProductQuantity(
        RemoveProductQuantityRequestModel request)
    {
        var isTaken = _productRepository.TakeProduct(request.Id, request.Quantity);

        if (isTaken)
        {
            var response = new RemoveProductQuantityResponseModel()
            {
                Message = "The product was successfully deleted."
            };

            return response;
        }

        return new BadRequestObjectResult("So many products are not in warehouse.");
    }
}