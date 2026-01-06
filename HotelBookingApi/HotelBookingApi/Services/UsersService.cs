using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using Mapster;

namespace HotelBookingApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repo;
        private readonly ITokenServices _tokenService;
        public UsersService(IUsersRepository repo, ITokenServices tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
            
        }

        public void AddUser(CreateUsersDto dto)
        {
            var user = dto.Adapt<Users>();
            user.CreatedAt = DateTime.UtcNow;

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _repo.Add(user);
            
        }

        public void DeleteUser(int Id)
        {
            _repo.Delete(Id);
        }

        public IEnumerable<UsersDto> GetAll()
        {
            var users = _repo.GetAll(); // returns IEnumerable<Users>

            // Map the collection using Mapster
            var usersDto = users.Adapt<IEnumerable<UsersDto>>();

            return usersDto;
        }

        public UsersDto GetById(int Id)
        {
            var users = _repo.GetById(Id);
            if (users == null)
                return null;

            var usersDto = users.Adapt<UsersDto>();

            return usersDto;
        }

        public void UpdateUser(int id, UpdateUsersDto users)
        {
            var user = _repo.GetById(id);

            user.FirstName = users.FirstName;
            user.LastName = users.LastName;
            user.Email = users.Email;
            

            _repo.Update(id, user);
        }

        string? IUsersService.Login(UsersLoginDto users)
        {
            var user = _repo.GetByEmail(users.Email);

            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(users.Password, user.Password);
            if (!isValid) return null;

            var tokenDto = new UsersTokenDto
            {
                Id = user.Id,
                Email = user.Email,
            };

            return _tokenService.GenerateToken(tokenDto);
        }
    }
}
