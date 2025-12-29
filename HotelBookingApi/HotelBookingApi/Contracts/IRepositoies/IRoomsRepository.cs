using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IRepositoies
{
    public interface IRoomsRepository
    {
        IEnumerable<Rooms> GetAll();
        Rooms GetById(int id);
        void Add(Rooms rooms);
        void Update(int id, Rooms rooms);
        void Delete(int id);

    }
}
