using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using HotelBookingApi.Models.DTOs;
using HotelBookingApi.Validator;
using Mapster;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelBookingApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repo;
        private readonly UserValidator _userValidator;
        public UsersService(IUsersRepository repo, UserValidator userValidator)
        {
            _repo = repo;
            _userValidator = userValidator;
        }

        public void AddUser(Users users)
        {
            if (_userValidator.userValidator(users, out var error))
            {
                throw new Exception(error);
            }

            _repo.Add(users);
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
    }
}
