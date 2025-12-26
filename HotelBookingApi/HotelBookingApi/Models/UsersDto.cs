namespace HotelBookingApi.Models
{
    public class UsersDto
    {
        //when you create a dto, always select a column in models that needed to be show
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string role { get; set; }
    }
}
