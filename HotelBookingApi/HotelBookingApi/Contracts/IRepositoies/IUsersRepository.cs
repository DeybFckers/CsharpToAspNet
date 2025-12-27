using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IRepositoies
{
    public interface IUsersRepository
    {
        IEnumerable<Users> GetAll();
        Users GetById(int Id);
        void Add(Users user);
    }
}
