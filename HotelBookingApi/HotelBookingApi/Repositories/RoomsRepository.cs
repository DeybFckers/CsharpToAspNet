using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Infrastructure;
using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly FakeDbContext _db;
        public RoomsRepository(FakeDbContext db)
        {
            _db = db;
        }

        public void Add(Rooms rooms)
        {
            rooms.Id = _db.Rooms.Count + 1;
            _db.Rooms.Add(rooms);
        }

        public void Delete(int id)
        {
            _db.Rooms.RemoveAll(room => room.Id == id);
        }

        public IEnumerable<Rooms> GetAll()
        {
            return _db.Rooms;
        }

        public Rooms GetById(int id)
        {
            return _db.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public void Update(int id, Rooms rooms)
        {
            var room = _db.Rooms.FirstOrDefault(r => r.Id == id);
            room.RoomNumber = rooms.RoomNumber;
            room.RoomType = rooms.RoomType;
            room.PricePerNight = rooms.PricePerNight;
            room.Capacity = rooms.Capacity;
            room.IsAvailable = rooms.IsAvailable;
            room.Description = rooms.Description;
        }
    }
}
