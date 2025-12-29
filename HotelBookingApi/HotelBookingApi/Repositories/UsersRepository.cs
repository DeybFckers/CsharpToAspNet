using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Infrastructure;
using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        //repository is just an raw data, if you want to add a logic, put it on service
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

        public void Delete(int Id)
        {
            _db.Users.RemoveAll(x => x.Id == Id);
        }

        public IEnumerable<Users> GetAll()
        {
            return _db.Users;
        }

        public Users GetById(int Id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == Id);
        }

        public void Update(int id, Users users)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            user.FirstName = users.FirstName;
            user.LastName = users.LastName;
            user.Email = users.Email;

        }
    }
}
