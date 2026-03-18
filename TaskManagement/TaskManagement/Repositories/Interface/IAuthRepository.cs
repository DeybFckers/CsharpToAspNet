using Microsoft.AspNetCore.Identity;
using TaskManagement.Data;
using TaskManagement.Models.DTOs.Auth;
using TaskManagement.Models.Entities;

namespace TaskManagement.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<ApplicationUser?> GetUserByEmail(string email);
        Task<IdentityResult> CreateUser(Register register);
        Task<bool> CheckPassword(ApplicationUser user, string password);
        Task<IList<string>> GetUserRoles(ApplicationUser user);
        Task<IdentityResult> AddRolesToUser(ApplicationUser user, IEnumerable<string> roles);
        Task SaveRefreshToken(RefreshToken token);
        Task<RefreshToken?> GetRefreshToken(string hashedToken);
        Task RevokeRefreshToken(RefreshToken token);
    }
}
