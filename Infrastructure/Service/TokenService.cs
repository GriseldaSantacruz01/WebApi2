using Core.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Core.Interfaces.Service;
namespace Infrastructure.Service;
public class TokenService : ITokenService
{

    public string GenerateToken(string name, string rol)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, name),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, rol)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthProperties.SecretKey));
        var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
                AuthProperties.Issuer,
                AuthProperties.Audience,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(AuthProperties.ExpirationTime),
                signInCredentials
        );
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}