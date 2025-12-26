using FirstWebApi.Models;

namespace FirstWebApi.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        bool IsValid(string token);
    }
    //then implement create TokenServices
}
