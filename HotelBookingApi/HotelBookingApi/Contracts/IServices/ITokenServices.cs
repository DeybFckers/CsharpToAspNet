using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IServices
{
    public interface ITokenServices
    {
        string GenerateToken(UsersTokenDto user);
    }
}
