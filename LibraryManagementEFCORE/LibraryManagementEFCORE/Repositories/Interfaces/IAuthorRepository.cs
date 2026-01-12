using LibraryManagementEFCORE.Models.Entities;

namespace LibraryManagementEFCORE.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task <IEnumerable<Author>> GetAllAsync();
        Task <Author> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author, int id);
        Task DeleteAsync(int id);

    }
}
