﻿using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.Order;

namespace Work_with_orders.Services.Order;

public interface IOrderService
{
    Task<ActionResult<IEnumerable<OrderViewModel>>> GetAllOrdersByEmail(string email);
    Task<ActionResult<OrderDetailsViewModel>> GetOrderDetails(long id, string email);
    Task<ActionResult<CreateOrderResponseModel>> CreateOrderByEmail(string email);
}