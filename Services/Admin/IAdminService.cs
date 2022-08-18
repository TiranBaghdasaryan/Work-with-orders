using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AdminModels;

namespace Work_with_orders.Services.Admin;

public interface IAdminService
{
    Task<IActionResult> FillUpUserBalanceByEmail(FillUpUserBalanceRequest request);
}