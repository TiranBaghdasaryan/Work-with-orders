﻿namespace Work_with_orders.Models.Order;

public class OrderViewModel
{
    public long OrderId { get; set; }
    public DateTime DoneDate { get; set; }
    public string Status { get; set; }
}