using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IRepositoies
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Add(Reservation reservation);
        void Update(int id, Reservation reservation);
        void Delete(int id);
    }
}
