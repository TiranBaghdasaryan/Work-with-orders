using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AdminModels;
using Work_with_orders.Services.Admin;

namespace Work_with_orders.Commands.Executors.AdminExecutor;

public class FillUpUserBalanceExecutor : IFillUpUserBalanceExecutor
{
    private FillUpUserBalanceRequest _request;
    private long _id;

    private IAdminService _adminService;

    public FillUpUserBalanceExecutor(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IFillUpUserBalanceExecutor WithParameter(long id, FillUpUserBalanceRequest parameter)
    {
        _request = parameter;
        _id = id;
        return this;
    }

    public async Task<IActionResult> ProcessExecution()
    {
        //to do validation

        return await _adminService.FillUpUserBalanceById(_id, _request);
    }
}