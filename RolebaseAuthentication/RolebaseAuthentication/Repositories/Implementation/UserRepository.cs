using Microsoft.AspNetCore.Identity;
using RolebaseAuthentication.Models;
using RolebaseAuthentication.Repositories.Interface;

namespace RolebaseAuthentication.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _manager;

        public UserRepository(UserManager<IdentityUser> manager)
        {
            _manager = manager;
        }

        public async Task<IdentityResult> AddUserToRole(IdentityUser user, string role)
        {
            return await _manager.AddToRoleAsync(user, role);   
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await _manager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUser(Register model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
            };
            return await _manager.CreateAsync(user, model.Password);
        }

        public async Task<IdentityUser?> GetUserByUsername(string username)
        {
            return await _manager.FindByNameAsync(username);
        }

        public async Task<IList<string>> GetUserRoles(IdentityUser user)
        {
            return await _manager.GetRolesAsync(user);
        }
    }
}
