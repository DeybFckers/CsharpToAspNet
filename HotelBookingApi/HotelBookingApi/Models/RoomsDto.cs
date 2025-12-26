namespace HotelBookingApi.Models
{
    public class RoomsDto
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }  
    }
}
