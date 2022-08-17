using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Work_with_orders.Entities;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Repositories;
using Work_with_orders.Services.Product;
using Xunit;

namespace Tests;

public class ProductServiceTests
{
    private Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();
    private Mock<IMapper> _mockMapper = new Mock<IMapper>();
    private IProductService _productService;

    private List<Product> _products = new List<Product>()
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

    [Fact]
    public async Task GetProductShouldReturns_OkObjectResult()
    {
        //arrange
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

        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));

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
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));

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
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));

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
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));

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
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));


        int productQuantity = GetById(id).Quantity;
        var addProductQuantityRequestModel = new AddProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 5,
        };

        //act
        var result = await _productService.AddProductQuantity(addProductQuantityRequestModel);
        var product = GetById(id);

        //assert
        _mockProductRepository.Verify(ex => ex.Update(GetById(id)), Times.Once);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Once);

        Assert.Equal(product.Quantity, productQuantity + addProductQuantityRequestModel.Quantity);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddProductQuantityShouldReturns_BadRequestObjectResult()
    {
        //arrange
        long id = 15;
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));

        var addProductQuantityRequestModel = new AddProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 5,
        };

        //act
        var result = await _productService.AddProductQuantity(addProductQuantityRequestModel);
        var product = GetById(id);

        //assert
        _mockProductRepository.Verify(ex => ex.Update(GetById(id)), Times.Never);
        _mockProductRepository.Verify(ex => ex.Save(), Times.Never);

        Assert.Equal(product, null);
        Assert.IsType<BadRequestObjectResult>(result);
    }


    //to-do

    [Fact]
    public async Task RemoveProductQuantityShouldReturns_OkObjectResult()
    {
        //arrange
        long id = 2;
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));

        int productQuantity = GetById(id).Quantity;
        var removeProductQuantityRequestModel = new RemoveProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 2,
        };

        _mockProductRepository.Setup(ex => ex.TakeProduct(id, removeProductQuantityRequestModel.Quantity))
            .Returns(true);

        //act
        var result = await _productService.RemoveProductQuantity(removeProductQuantityRequestModel);
        var product = GetById(id);


        //assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RemoveProductQuantityShouldReturns_BadRequestObjectResult()
    {
        //arrange
        long id = 46;
        _productService = new ProductService(_mockProductRepository.Object, _mockMapper.Object);
        _mockProductRepository.Setup(ex => ex.GetById(id)).ReturnsAsync(GetById(id));

        var removeProductQuantityRequestModel = new RemoveProductQuantityRequestModel()
        {
            Id = id,
            Quantity = 2,
        };

        _mockProductRepository.Setup(ex => ex.TakeProduct(id, removeProductQuantityRequestModel.Quantity))
            .Returns(false);

        //act
        var result = await _productService.RemoveProductQuantity(removeProductQuantityRequestModel);
        var product = GetById(id);


        //assert
        Assert.Equal(product, null);
        Assert.IsType<BadRequestObjectResult>(result);
    }


    private Product GetById(long id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);

        if (product != null)
        {
            return product;
        }

        return null;
    }
}