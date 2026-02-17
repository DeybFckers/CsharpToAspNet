using RolebaseAuthentication.Models;

namespace RolebaseAuthentication.Services.Interface
{
    public interface IAuthServices
    {
        Task<string?> LoginAsync(Login model);
        Task<bool> RegisterAsync(Register model);
    }
}
