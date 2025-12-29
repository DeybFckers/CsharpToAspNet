using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using Mapster;

namespace HotelBookingApi.Services
{
    public class RoomsServices : IRoomsServices
    {
        private readonly IRoomsRepository _repo;
        public RoomsServices(IRoomsRepository repo)
        {
            _repo = repo;
        }
        public void AddRooms(CreateRoomsDto rooms)
        {
            var room = rooms.Adapt<Rooms>();
            _repo.Add(room);
        }

        public void DeleteRooms(int id)
        {
            _repo.Delete(id);
        }

        public IEnumerable<RoomsDto> GetAll()
        {
            var rooms = _repo.GetAll();

            var roomsDto = rooms.Adapt<IEnumerable<RoomsDto>>();
            return roomsDto;
        }

        public RoomsDto GetById(int id)
        {
            var rooms = _repo.GetById(id);

            var roomsDto = rooms.Adapt<RoomsDto>();

            return roomsDto;
        }

        public void UpdateRooms(int id, UpdateRoomsDto rooms)
        {
            var room = _repo.GetById(id);
            room.RoomNumber = rooms.RoomNumber;
            room.RoomType = rooms.RoomType;
            room.PricePerNight = rooms.PricePerNight;
            room.Capacity = rooms.Capacity;
            room.IsAvailable = rooms.IsAvailable;
            room.Description = rooms.Description;

            _repo.Update(id, room);
        }
    }
}
