using HotelBookingApi.Models;

namespace HotelBookingApi.Infrastructure
{
    public class FakeDbContext
    {
        public FakeDbContext()
        {
            Users = new List<Users>();
            Rooms = new List<Rooms>();
            Reservation = new List<Reservation>();  
        }
        public List<Users> Users { get; set; }
        public List<Rooms> Rooms { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}
