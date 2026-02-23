using TaskManagement.Models.DTOs.Auth;

namespace TaskManagement.Services.Interface
{
    public interface IAuthServices
    {
        Task<AuthResponseDto> Login(Login login);
        Task<bool> Register(Register register);
    }
}
