using Microsoft.AspNetCore.Identity;
using TaskManagement.Data;
using TaskManagement.Services.Interface;
using TaskManagement.Repositories.Interface;
using TaskManagement.Models.DTOs.Auth;
using Mapster;


namespace TaskManagement.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public UserServices(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(bool hideAdmins)
        {
            var users = await _userRepository.GetAllUsers();

            var results = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (hideAdmins && roles.Contains("Admin"))
                    continue;

                results.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    fullname = new UserFullnameDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    },
                    Roles = roles.ToArray()
                });
            }

            return results;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null) return null;

            return user.Adapt<UserDto>();
        }

        public async Task<IEnumerable<UserDto>> GetUserByRole(string role)
        {
            var user = await _userRepository.GetUserByRole(role);

            if (user == null) return null;

            var userDtos = new List<UserDto>();

            foreach (var u in user)
            {
                var roles = await _userManager.GetRolesAsync(u);

                userDtos.Add(new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    fullname = new UserFullnameDto
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    },
                    Roles = roles.ToArray()
                });
            }

            return userDtos;
        }
    }
}
