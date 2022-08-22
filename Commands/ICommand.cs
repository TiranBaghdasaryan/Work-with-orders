using Microsoft.AspNetCore.Mvc;

namespace Work_with_orders.Commands;

public interface ICommand
{
    Task<IActionResult> Execute();
}