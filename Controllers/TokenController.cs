using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.Authentication;
using Work_with_orders.Repositories;
using Work_with_orders.Services.Token;

namespace Work_with_orders.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserRepository _userRepository;

    public TokenController(ITokenService tokenService, UserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(TokenModel tokenModel)
    {
        string accessToken = tokenModel.AccessToken;
        string refreshToken = tokenModel.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        string email = (HttpContext.User.Claims.FirstOrDefault(x => Equals(x.Type, ClaimTypes.Email))!.Value);
        var user = await _userRepository.GetByEmailAsync(email);

        if (Equals(user,null) || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid client request");   
        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await _userRepository.SaveChangesAsync();
        return Ok(new TokenModel(newAccessToken,newRefreshToken));
    }

    [HttpPost]
    [Authorize]
    [Route("revoke")]
    public async Task<IActionResult> Revoke()
    {
        string email = (HttpContext.User.Claims.FirstOrDefault(x => Equals(x.Type, ClaimTypes.Email))!.Value);
        var user = await _userRepository.GetByEmailAsync(email);
        if (Equals(user, null)) return BadRequest();
        user.RefreshToken = null;
        await _userRepository.SaveChangesAsync();
        return NoContent();
    }
}