namespace HotelBookingApi.Models.DTOs
{
    public class ReservationDto
    {
        //when you create a dto, always select a column in models that needed to be show
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
    }
}
