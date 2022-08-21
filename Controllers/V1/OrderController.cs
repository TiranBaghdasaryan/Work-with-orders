using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.Order;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Authorize(Roles = "User")]
[Route("v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAllOrders()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var response = await _orderService.GetAllOrdersByEmail(email);

        return response;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailsViewModel>> GetOrderDetailsById(long id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var response = await _orderService.GetOrderDetails(id, email);

        return response;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var response = await _orderService.CreateOrderByEmail(email);

        return response;
    }

    [HttpPatch("{id}")]
    public IActionResult RejectOrder(long id)
    {
        return null; // to do 
    }
}