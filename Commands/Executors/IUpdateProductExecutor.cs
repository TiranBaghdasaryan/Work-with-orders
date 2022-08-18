﻿using Work_with_orders.Models.ProductModels.UpdateProduct;

namespace Work_with_orders.Commands.Executors;

public interface IUpdateProductExecutor : ICommand
{
    public IUpdateProductExecutor WithParameter(UpdateProductRequestModel parameter);
}