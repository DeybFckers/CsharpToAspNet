using LibraryManagementEFCORE.Models.Entities;

namespace LibraryManagementEFCORE.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task <IEnumerable<Author>> GetAllAuthorAsync();
        Task <Author> GetByAuthorIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);

    }
}
