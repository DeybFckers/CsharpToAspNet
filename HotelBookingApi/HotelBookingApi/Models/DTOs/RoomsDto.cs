namespace HotelBookingApi.Models.DTOs
{
    public class RoomsDto
    {
        //when you create a dto, always select a column in models that needed to be show
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }  
    }
}
