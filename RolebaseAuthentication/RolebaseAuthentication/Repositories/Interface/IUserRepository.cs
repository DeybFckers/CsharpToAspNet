using Microsoft.AspNetCore.Identity;
using RolebaseAuthentication.Models;

namespace RolebaseAuthentication.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IdentityUser?> GetUserByUsername(string username);
        Task<IdentityResult> CreateUser(Register model);
        Task<bool> CheckPassword(IdentityUser user, string password);
        Task<IList<string>>GetUserRoles(IdentityUser user);
        Task<IdentityResult> AddUserToRole(IdentityUser user, string role);
    }
}
