using Microsoft.AspNetCore.Mvc;

namespace Work_with_orders.Commands;

public interface ICommand<T>
{
    T Validator { get; set; }
    void Validation();
    Task<IActionResult> ProcessExecution();

    Task<IActionResult> Execute()
    {
        Validation();
        return ProcessExecution();
    }
}