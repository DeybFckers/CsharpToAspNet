using HotelBookingApi.Contracts;

namespace HotelBookingApi.Models
{
    public class Users
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    }
}
