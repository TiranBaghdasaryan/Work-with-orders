using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AdminModels;
using Work_with_orders.Services.Admin;

namespace Work_with_orders.Commands.Executors.AdminExecutor;

public class FillUpUserBalanceExecutor : IFillUpUserBalanceExecutor
{
    private FillUpUserBalanceRequest _request;

    private IAdminService _adminService;

    public FillUpUserBalanceExecutor(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IFillUpUserBalanceExecutor WithParameter(FillUpUserBalanceRequest parameter)
    {
        _request = parameter;
        return this;
    }

    public async Task<IActionResult> ProcessExecution()
    {
        //to do validation

        return await _adminService.FillUpUserBalanceByEmail(_request);
    }
}