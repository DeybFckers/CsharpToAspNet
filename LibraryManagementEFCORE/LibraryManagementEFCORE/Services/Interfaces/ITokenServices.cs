using LibraryManagementEFCORE.Models.DTOs;

namespace LibraryManagementEFCORE.Services.Interfaces
{
    public interface ITokenServices
    {
        string GenerateToken(int id, string email);
    }
}
