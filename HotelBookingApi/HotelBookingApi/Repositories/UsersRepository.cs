using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Infrastructure;
using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly FakeDbContext _db;
        public UsersRepository(FakeDbContext db)
        {
            _db = db;
        }

        public void Add(Users user)
        {
            user.Id = _db.Users.Count + 1;
            _db.Users.Add(user);
        }

        public IEnumerable<Users> GetAll()
        {
            return _db.Users;
        }

        public Users GetById(int Id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == Id);
        }
    }
}
