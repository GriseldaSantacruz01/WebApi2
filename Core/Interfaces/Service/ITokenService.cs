namespace Core.Interfaces.Service
{
    public interface ITokenService
    {
        string GenerateToken(string name, string rol);
    }
}