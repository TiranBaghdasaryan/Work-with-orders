using System.Security.Claims;
using Work_with_orders.Enums;

namespace Work_with_orders.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Claim[] SetClaims(string email, Role role);
}