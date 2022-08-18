using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Executors.AdminExecutor;
using Work_with_orders.Commands.Executors.AdminExecutor.BlockUser;
using Work_with_orders.Models.AdminModels;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Route("v1/admin")]
public class AdminController
{
    [HttpPut("users/{id}/balance")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FillUpUserBalance([FromServices] IFillUpUserBalanceExecutor executor, long id,
        FillUpUserBalanceRequest request)
    {
        var response = await executor.WithParameter(id, request).Execute();
        return response;
    }

    [HttpPatch("users/{id}/block")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BlockUser([FromServices] IBlockUserExecutor executor, long id)
    {
        var response = await executor.WithParameter(id).Execute();
        return response;
    }
}