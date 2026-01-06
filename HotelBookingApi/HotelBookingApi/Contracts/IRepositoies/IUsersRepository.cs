using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IRepositoies
{
    public interface IUsersRepository
    {
        IEnumerable<Users> GetAll();
        Users GetById(int Id);
        void Add(Users user);
        void Update(int id, Users user);
        void Delete(int Id);
        Users? GetByEmail(string email);
    }
}
