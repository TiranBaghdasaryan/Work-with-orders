using Microsoft.AspNetCore.Mvc;
using Moq;
using Work_with_orders.Commands.Executors;
using Work_with_orders.Controllers.V1;
using Work_with_orders.Services.Product;
using Xunit;

namespace Tests;

public class ProductControllerTests
{
    private ProductController _productController;
    private Mock<IProductService> _productServiceMock = new Mock<IProductService>();
    private Mock<IGetProductExecutor> _getProductExecutorMock = new Mock<IGetProductExecutor>();

    [Fact]
    public async Task GetProductShouldReturns_OkObjectResult()
    {
        //arrange 
        _productController = new ProductController(_productServiceMock.Object);

        long id = 5;

        _getProductExecutorMock.Setup(ex => ex.WithParameter(id).Execute())
            .ReturnsAsync(new OkObjectResult(""));

        //act
        var result = await _productController.GetProduct(_getProductExecutorMock.Object, id);

        //assert
        _getProductExecutorMock.Verify(ex => ex.WithParameter(id).Execute(), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProductShouldReturns_BadRequestObjectResult()
    {
        //arrange 
        _productController = new ProductController(_productServiceMock.Object);

        long id = 15;

        _getProductExecutorMock.Setup(ex => ex.WithParameter(id).Execute())
            .ReturnsAsync(new BadRequestObjectResult(""));

        //act
        var result = await _productController.GetProduct(_getProductExecutorMock.Object, id);

        //assert
        _getProductExecutorMock.Verify(ex => ex.WithParameter(id).Execute(), Times.Once);
        Assert.IsType<BadRequestObjectResult>(result);
    }
}