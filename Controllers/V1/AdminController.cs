using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Executors.AdminExecutor;
using Work_with_orders.Models.AdminModels;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Route("v1/admin")]
public class AdminController
{
    [HttpPut("balance")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FillUpUserBalance([FromServices] IFillUpUserBalanceExecutor executor,
        FillUpUserBalanceRequest request)
    {
        var response = await executor.WithParameter(request).Execute();
        return response;
    }
}