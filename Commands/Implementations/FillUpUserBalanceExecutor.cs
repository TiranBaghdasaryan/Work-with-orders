using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Commands.Implementations;

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

    public async Task<IActionResult> Execute()
    {
        //to do validation

        return await _adminService.FillUpUserBalanceById(_id, _request);
    }
}