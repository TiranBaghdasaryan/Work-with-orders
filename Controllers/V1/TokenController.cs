using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AuthenticationModels.RefreshToken;
using Work_with_orders.Repositories;
using Work_with_orders.Services.Token;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Route("v1/token")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public TokenController(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult<RefreshTokenResponseModel>> RefreshToken(RefreshTokenRequestModel tokenModel)
    {
        string accessToken = tokenModel.AccessToken;
        string refreshToken = tokenModel.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        string email = (HttpContext.User.Claims.FirstOrDefault(x => Equals(x.Type, ClaimTypes.Email))!.Value);
        var user = await _userRepository.GetByEmailAsync(email);

        if (Equals(user, null) || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid client request");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userRepository.Save();

        var response = new RefreshTokenResponseModel()
        {
            Tokens = new Dictionary<string, string>()
            {
                { "accessToken", newAccessToken },
                { "refreshToken", newRefreshToken }
            }
        };

        return response;
    }

    [HttpPost]
    [Authorize]
    [Route("revoke")]
    public async Task<IActionResult> RevokeAsync()
    {
        string email = (HttpContext.User.Claims.FirstOrDefault(x => Equals(x.Type, ClaimTypes.Email))!.Value);
        var user = await _userRepository.GetByEmailAsync(email);

        if (Equals(user, null))
        {
            return BadRequest();
        }

        user.RefreshToken = null;
        await _userRepository.Save();

        return NoContent();
    }
}