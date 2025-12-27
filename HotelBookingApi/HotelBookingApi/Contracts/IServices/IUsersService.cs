using HotelBookingApi.Models;
using HotelBookingApi.Models.DTOs;

namespace HotelBookingApi.Contracts.IServices
{
    public interface IUsersService
    {
        IEnumerable<UsersDto> GetAll();
        UsersDto GetById(int Id);
        void AddUser(Users users);
    }
}
