using HotelBookingApi.Models;

namespace HotelBookingApi.Contracts.IServices
{
    public interface IRoomsServices
    {
        IEnumerable<RoomsDto> GetAll();
        RoomsDto GetById(int id);
        void AddRooms(CreateRoomsDto rooms);
        void UpdateRooms(int id, UpdateRoomsDto rooms);
        void DeleteRooms(int id);
    }
}
