using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Validations.Manual_Validations;

namespace Work_with_orders.Commands.Executors;

public interface IGetProductExecutor
{
    Task<IActionResult> Execute();
    public ICommand<CheckProductByIdValidation> WithParameter(long parameter);
}


