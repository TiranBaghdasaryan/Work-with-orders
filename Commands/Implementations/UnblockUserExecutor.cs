using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Commands.Implementations;

public class UnblockUserExecutor : IUnblockUserExecutor
{
    private long _id;

    private IAdminService _adminService;

    public UnblockUserExecutor(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IUnblockUserExecutor WithParameter(long id)
    {
        _id = id;
        return this;
    }

    public async Task<IActionResult> Execute()
    {
        // to do validation
        return await _adminService.UnblockUserById(_id);
    }
}