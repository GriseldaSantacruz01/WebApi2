using Core.Entities;
using System.Security.Claims;
namespace Core.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(string name, string rol);
    }
}