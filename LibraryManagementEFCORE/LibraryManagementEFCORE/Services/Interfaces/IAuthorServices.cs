using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;

namespace LibraryManagementEFCORE.Services.Interfaces
{
    public interface IAuthorServices
    {
        Task<IEnumerable<AuthorProfileDto>> GetAllAsync();
        Task<AuthorProfileDto> GetByIdAsync(int id);
        Task AddAsync(AuthorCreateDto author);
        Task DeleteAsync(int id);
        Task<bool> UpdateAsync(AuthorUpdateDto author, int id);

    }
}
