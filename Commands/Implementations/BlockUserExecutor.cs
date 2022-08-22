using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Commands.Implementations;

public class BlockUserExecutor : IBlockUserExecutor
{
    private long _id;
    private IAdminService _adminService;

    public BlockUserExecutor(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IBlockUserExecutor WithParameter(long id)
    {
        _id = id;
        return this;
    }

    public async Task<IActionResult> Execute()
    {
        return await _adminService.BlockUserById(_id);
    }
}