using HotelBookingApi.Contracts;
using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUserRepository
    {
        public UsersRepository() : base()
        {

        }
    }
}
