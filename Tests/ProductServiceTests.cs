using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Work_with_orders.Entities;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Repositories;
using Work_with_orders.Services.Implementations;
using Work_with_orders.Services.Interfaces;
using Xunit;

namespace Tests;

public class ProductServiceTests
{
    private Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();
    private Mock<IMapper> _mockMapper = new Mock<IMapper>();
    private IProductService _productService;

    [Fact]
    public async Task GetProductShouldReturns_OkObjectResult()
    {
        //arrange
        long id = 1;

        var product = new Product()
        {
            Id = id,
            Quantity = 5,
        };

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        //act 

        var result = await _productService.GetProductById(id);

        //assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProductByIdShouldReturns_BadRequestObjectResult()
    {
        //arrange
        long id = 64;

        Product product = null;

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        //act 
        var result = await _productService.GetProductById(id);

        //assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetProductsShouldReturns_OkObjectResult()
    {
        //arrange
        List<Product> _products = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Quantity = 5,
            },
            new Product()
            {
                Id = 2,
                Quantity = 6,
            },
            new Product()
            {
                Id = 3,
                Quantity = 10,
            },
            new Product()
            {
                Id = 4,
                Quantity = 15,
            },
        };

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetAll()).Returns(_products);

        //act
        var result = await _productService.GetProducts();

        //assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task CreateProductShouldReturns_OkObjectResult()
    {
        //arrange
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);


        //act
        var result = await _productService.CreateProduct(It.IsAny<CreateProductRequestModel>());


        //assert
        _mockProductRepository.Verify(ex => ex.Add(It.IsAny<Product>()), Times.Once);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProductShouldReturns_OkObjectResult()
    {
        //arrange
        long id = 1;

        var product = new Product()
        {
            Id = id,
            Quantity = 5,
        };

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        //act
        var result = await _productService.UpdateProduct(new UpdateProductRequestModel()
        {
            Id = id,
            Name = "Apple",
        });

        //assert
        _mockProductRepository.Verify(ex => ex.Update(It.IsAny<Product>()), Times.Once);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Once);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProductShouldReturns_BadRequestObjectResult()
    {
        //arrange
        long id = 6;

        var product = new Product()
        {
            Id = id,
            Quantity = 5,
        };

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        //act
        var result = await _productService.UpdateProduct(new UpdateProductRequestModel()
        {
            Id = 1,
            Name = "Apple",
        });

        //assert
        _mockProductRepository.Verify(ex => ex.Update(It.IsAny<Product>()), Times.Never);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Never);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProductByIdShouldReturns_OkObjectResult()
    {
        //arrange
        long id = 1;

        var product = new Product()
        {
            Id = id,
            Quantity = 5,
        };

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        _mockProductRepository.Setup(ex => ex.Delete(product));

        //act
        var result = await _productService.DeleteProductById(id);

        //assert
        _mockProductRepository.Verify(ex => ex.Delete(It.IsAny<Product>()), Times.Once);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteProductByIdShouldReturns_BadRequestObjectResult()
    {
        //arrange
        long id = 10;

        var product = new Product()
        {
            Id = id,
            Quantity = 5,
        };

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(() => null);

        //act
        var result = await _productService.DeleteProductById(id);

        //assert
        _mockProductRepository.Verify(ex => ex.Delete(It.IsAny<Product>()), Times.Never);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Never);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddProductQuantityShouldReturns_OkObjectResult()
    {
        //arrange
        long id = 2;

        var product = new Product()
        {
            Id = id,
            Quantity = 5,
        };
        
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        int productQuantity = product.Quantity;
        var addProductQuantityRequestModel = new AddProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 5,
        };

        //act
        var result = await _productService.AddProductQuantity(addProductQuantityRequestModel);

        //assert
        _mockProductRepository.Verify(ex => ex.Update(product), Times.Once);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Once);

        Assert.Equal(product.Quantity, productQuantity + addProductQuantityRequestModel.Quantity);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddProductQuantityShouldReturns_BadRequestObjectResult()
    {
        //arrange
        long id = 15;
        Product product = null;

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        var addProductQuantityRequestModel = new AddProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 5,
        };

        //act
        var result = await _productService.AddProductQuantity(addProductQuantityRequestModel);

        //assert
        _mockProductRepository.Verify(ex => ex.Save(), Times.Never);

        Assert.Equal(product, null);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RemoveProductQuantityShouldReturns_OkObjectResult()
    {
        //arrange
        long id = 2;

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);

        var removeProductQuantityRequestModel = new RemoveProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 2,
        };

        _mockProductRepository.Setup(ex => ex.TakeProduct(id, removeProductQuantityRequestModel.Quantity))
            .Returns(true);

        //act
        var result = await _productService.RemoveProductQuantity(removeProductQuantityRequestModel);
        
        //assert
        _mockProductRepository.Verify(ex => ex.TakeProduct(removeProductQuantityRequestModel.Id,removeProductQuantityRequestModel.Quantity), Times.Once);

        Assert.IsType<OkObjectResult>(result);
    }

    //
    [Fact]
    public async Task RemoveProductQuantityShouldReturns_BadRequestObjectResult()
    {
        //arrange
        long id = 46;

        Product product = null;
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(product);

        var removeProductQuantityRequestModel = new RemoveProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 2,
        };

        _mockProductRepository.Setup(ex => ex.TakeProduct(id, removeProductQuantityRequestModel.Quantity))
            .Returns(false);

        //act
        var result = await _productService.RemoveProductQuantity(removeProductQuantityRequestModel);

        //assert
        _mockProductRepository.Verify(ex => ex.TakeProduct(removeProductQuantityRequestModel.Id,removeProductQuantityRequestModel.Quantity), Times.Exactly(1));
        Assert.IsType<BadRequestObjectResult>(result);
    }
}