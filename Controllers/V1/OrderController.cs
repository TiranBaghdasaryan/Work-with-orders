using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Enums;
using Work_with_orders.Models.Order;
using Work_with_orders.Models.Product;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.BasketProductRepo;
using Work_with_orders.Repositories.OrderProductRepo;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Authorize(Roles = "User")]
[Route("v1/orders")]
public class OrderController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly BasketRepository _basketRepository;
    private readonly BasketProductRepository _basketProductRepository;
    private readonly OrderRepository _orderRepository;
    private readonly OrderProductRepository _orderProductRepository;
    private readonly ProductRepository _productRepository;
    private readonly ApplicationContext _context;


    public OrderController
    (UserRepository userRepository,
        BasketRepository basketRepository,
        BasketProductRepository basketProductRepository,
        OrderRepository orderRepository,
        OrderProductRepository orderProductRepository,
        ProductRepository productRepository,
        ApplicationContext context)
    {
        _userRepository = userRepository;
        _basketRepository = basketRepository;
        _basketProductRepository = basketProductRepository;
        _orderRepository = orderRepository;
        _orderProductRepository = orderProductRepository;
        _productRepository = productRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAllOrders()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userRepository.GetByEmailAsync(email);

        var orders = await _orderRepository.GetOrdersByUserId(user.Id);

        var responseModel = new List<OrderViewModel>(orders.Count);
        foreach (var order in orders)
        {
           responseModel.Add(new OrderViewModel()
           {
               OrderId = order.Id,
               DoneDate = order.DoneDate,
               Status =  ((OrderStatus)order.Status).ToString()
           }); 
        }
        

        return Ok(responseModel);
    }

    [HttpGet("{id}")]
    public ActionResult<OrderViewModel> GetOrderById(long id)
    {
        return null;
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (!Equals(basket, null))
        {
            var productsInBasket = await _basketProductRepository.GetAllProductsInBasket(basket.Id);
            var productsCountInBasket = productsInBasket.Count;

            if (productsCountInBasket < 1)
            {
                return BadRequest("The basket is empty.");
            }

            var order = new Order()
            {
                UserId = user.Id,
            };

            await _orderRepository.Add(order);
            await _orderRepository.Save();

            var responseModel = new CreateOrderResponseModel();
            foreach (var item in productsInBasket)
            {
                bool isTaken = _productRepository.TakeProduct(item.ProductId, item.Quantity);
                var product = await _productRepository.GetById(item.ProductId);

                var productId = item.ProductId;
                var productName = product.Name;
                var productQuantity = item.Quantity;

                if (isTaken)
                {
                    await _orderProductRepository.AddProductInOrder(order.Id, item.ProductId, item.Quantity);

                    responseModel.ProductsOrderedSuccessfully.Add(new ProductInOrderViewModel()
                    {
                        ProductId = productId,
                        ProductName = productName,
                        ProductQuantity = productQuantity
                    });
                }
                else
                {
                    responseModel.ProductsOrderedFailed.Add(new ProductInOrderViewModel()
                    {
                        ProductId = productId,
                        ProductName = productName,
                        ProductQuantity = productQuantity
                    });
                }
            }

            _basketProductRepository.RemoveAllProductsFromBasket(basket.Id);
            await _orderProductRepository.Save();

            return Ok(new
            {
                OrderedSuccessfully = responseModel.ProductsOrderedSuccessfully,
                OrderedFailed = responseModel.ProductsOrderedFailed,
            });
        }
        else
        {
            return BadRequest("The basket is empty.");
        }
    }

    [HttpPatch]
    public IActionResult RejectOrder()
    {
        return null;
    }
}