using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IServices
{
    public interface IReservationServices
    {
        IEnumerable<ReservationDto> GetAll();
        ReservationDto GetById(int id);
        void AddReservation(CreateReservationDto reservation);
        void UpdateReservation(int id, UpdateReservationDto reservation);
        void DeleteReservation(int id);
    }
}
