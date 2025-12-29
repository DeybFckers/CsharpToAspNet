namespace HotelBookingApi.Models
{
    public class Rooms
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public String Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class RoomsDto
    {
        //when you create a dto, always select a column in models that needed to be show
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public String Description { get; set; }
    }

    public class CreateRoomsDto
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public String Description { get; set; }
    }

    public class UpdateRoomsDto
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public String Description { get; set; }
    }
}
