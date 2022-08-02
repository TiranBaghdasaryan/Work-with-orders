using System.Security.Claims;
using Work_with_orders.Enums;

namespace Work_with_orders.Services.Token;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    Claim[] SetClaims(string email, Role role);
}