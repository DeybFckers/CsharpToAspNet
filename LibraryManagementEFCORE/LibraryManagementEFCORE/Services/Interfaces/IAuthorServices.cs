using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;

namespace LibraryManagementEFCORE.Services.Interfaces
{
    public interface IAuthorServices
    {
        Task<IEnumerable<AuthorProfileDto>> GetAllAuthorAsync();
        Task<AuthorProfileDto> GetByAuthorIdAsync(int id);
        Task AddAuthorAsync(AuthorCreateDto author);
        Task DeleteAuthorAsync(int id);
        Task<bool> UpdateAuthorAsync(AuthorUpdateDto author, int id);

    }
}
