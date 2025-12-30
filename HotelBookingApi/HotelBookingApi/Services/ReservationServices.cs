using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using Mapster;

namespace HotelBookingApi.Services
{
    public class ReservationServices : IReservationServices
    {
        private readonly IReservationRepository _repo;
        private readonly IRoomsRepository _roomRepo;
        private readonly IUsersRepository _usersRepo;

        public ReservationServices(IReservationRepository repo, IRoomsRepository roomRepo, IUsersRepository usersRepo)
        {
            _repo = repo;
            _roomRepo = roomRepo;
            _usersRepo = usersRepo; 
        }

        public void AddReservation(CreateReservationDto reservation)
        {
            var reservations = reservation.Adapt<Reservation>();
            var room = _roomRepo.GetById(reservation.RoomId);
            var user = _usersRepo.GetById(reservation.UserId);

            if(user == null)
            {
                throw new Exception("The User does not Exists");
            }

            if(room == null)
            {
                throw new Exception("The Room does not Exists");
            }

            if (reservations.CheckOutDate <= reservation.CheckInDate)
            {
                throw new Exception("The Check-Out Date must be after Check-In Date");
            }

            int totalDays = (reservations.CheckOutDate - reservations.CheckInDate).Days;

            reservations.TotalPrice = room.PricePerNight * totalDays;

            reservations.CreatedAt = DateTime.UtcNow;

            if (!room.IsAvailable)
            {
                throw new Exception("The Room is already Booked");
            }

            room.IsAvailable = false;

            _repo.Add(reservations);
        }

        public void DeleteReservation(int id)
        {
            _repo.Delete(id);
        }

        public IEnumerable<ReservationDto> GetAll()
        {
            var reservations = _repo.GetAll();
            
            var reservationsDto = reservations.Adapt<IEnumerable<ReservationDto>>();

            return reservationsDto;
        }

        public ReservationDto GetById(int id)
        {
            var reservations = _repo.GetById(id);

            var reservationsDto = reservations.Adapt<ReservationDto>();

            return reservationsDto;
        }

        public void UpdateReservation(int id, UpdateReservationDto reservation)
        {
            var reservations = _repo.GetById(id);
            var room = _roomRepo.GetById(id);

            var newTotalDays = (reservations.CheckOutDate - reservations.CheckInDate).Days;

            var newTotalPrice = newTotalDays * room.PricePerNight;

            reservations.UserId = reservation.UserId;
            reservations.RoomId = reservation.RoomId;
            reservations.CheckInDate = reservation.CheckInDate;
            reservations.CheckOutDate = reservation.CheckOutDate;
            reservations.Status = reservation.Status;

            reservations.TotalPrice = newTotalPrice;

            
        }
    }
}
