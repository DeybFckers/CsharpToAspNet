using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Infrastructure;
using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly FakeDbContext _db;

        public ReservationRepository(FakeDbContext db)
        {
            _db = db;
        }

        public void Add(Reservation reservation)
        {
            reservation.Id = _db.Reservation.Count + 1;
            _db.Reservation.Add(reservation);
        }

        public void Delete(int id)
        {
            _db.Reservation.RemoveAll(r => r.Id == id);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _db.Reservation;
        }

        public Reservation GetById(int id)
        {
            return _db.Reservation.FirstOrDefault(r => r.Id == id);
        }

        public void Update(int id, Reservation reservation)
        {
            var reservations = _db.Reservation.FirstOrDefault(r => r.Id == id);
            reservations.UserId = reservation.UserId;
            reservations.RoomId = reservation.RoomId;
            reservations.CheckInDate = reservation.CheckInDate;
            reservations.CheckOutDate = reservation.CheckOutDate;
            reservations.Status = reservation.Status;
        }
    }
}
