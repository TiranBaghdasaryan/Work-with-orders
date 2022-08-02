using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Work_with_orders.Enums;
using Work_with_orders.Options;

namespace Work_with_orders.Services.Token;

public class TokenService : ITokenService
{

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.SecretKey));

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(JwtOptions.ExpiryMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        Console.WriteLine(jwtToken);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public Claim[] SetClaims(string email, Role role)
    {
        return new Claim[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role.ToString())
        };
    }
}