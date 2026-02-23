using TaskManagement.Data;
using TaskManagement.Models.DTOs.Auth;

namespace TaskManagement.Services.Interface
{
    public interface IUserServices
    {
        Task<IEnumerable<UserDto>> GetAllUsers(bool hideAdmins);
        Task<UserDto> GetUserById(Guid id);
        Task<IEnumerable<UserDto>> GetUserByRole(string role);
    }
}
