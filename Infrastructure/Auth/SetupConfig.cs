using Core.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
namespace Infrastructure.Auth
{
    public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
    {
        public void Configure(JwtBearerOptions options)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthProperties.SecretKey));
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = AuthProperties.Issuer,
                ValidAudience = AuthProperties.Audience,
                IssuerSigningKey = issuerSigningKey
            };
        }

    }
}