using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AdminModels;

namespace Work_with_orders.Services.Interfaces;

public interface IAdminService
{
    Task<IActionResult> FillUpUserBalanceById(long id, FillUpUserBalanceRequest request);
    Task<IActionResult> BlockUserById(long id);
    Task<IActionResult> UnblockUserById(long id);
    
    
}