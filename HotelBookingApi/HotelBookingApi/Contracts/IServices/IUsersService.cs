using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IServices
{
    public interface IUsersService
    {
        IEnumerable<UsersDto> GetAll();
        UsersDto GetById(int Id);
        void AddUser(CreateUsersDto users);
        void UpdateUser(int id, UpdateUsersDto users);
        void DeleteUser(int Id);
        string? Login(UsersLoginDto users);
    }
}
