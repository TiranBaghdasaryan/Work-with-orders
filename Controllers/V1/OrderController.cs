using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Enums;
using Work_with_orders.Models.Order;
using Work_with_orders.Models.ProductModels.ViewModels;
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
                Status = ((OrderStatus)order.Status).ToString()
            });
        }


        return responseModel;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailsViewModel>> GetOrderById(long id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userRepository.GetByEmailAsync(email);
        
        var order = await _orderRepository.GetOrderById(id);

        if (Equals(order, null) || !Equals(order.UserId, user.Id))
        {
            return StatusCode(401);
        }

        var productsInOrderViewModels = new List<ProductInOrderViewModel>();

        var productsInOrder = await _orderProductRepository.GetProductsByOrderId(order.Id);

        foreach (var orderProduct in productsInOrder)
        {
            var product = await _productRepository.GetById(orderProduct.ProductId);
            var productId = product.Id;
            var productName = product.Name;
            var productQuantity = orderProduct.Quantity;
        
            productsInOrderViewModels.Add(new ProductInOrderViewModel()
            {
                ProductId = productId,
                ProductName = productName,
                ProductQuantity = productQuantity,
            });
        }
        
        var orderDetails = new OrderDetailsViewModel()
        {
            OrderId = order.Id,
            DoneDate = order.DoneDate,
            Status = ((OrderStatus)order.Status).ToString(),
            ProductsViewModels = productsInOrderViewModels,
        };
        
        return Ok(orderDetails);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateOrderResponseModel>> CreateOrder()
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

            return responseModel;
        }
        else
        {
            return BadRequest("The basket is empty.");
        }
    }
    
    [HttpPatch("{id}")]
    public IActionResult RejectOrder(long id)
    {
        return null; // to do 
    }
}