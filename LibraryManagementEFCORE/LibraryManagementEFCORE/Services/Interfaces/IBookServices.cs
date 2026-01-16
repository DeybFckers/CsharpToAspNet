using LibraryManagementEFCORE.Models.DTOs;

namespace LibraryManagementEFCORE.Services.Interfaces
{
    public interface IBookServices
    {
        Task<IEnumerable<BookProfileDto>> GetAllBookAsync();
        Task <BookProfileDto> GetByBookIdAsync(int id);
        Task AddBookAsync(BookCreateDto book);
        Task <bool>UpdateBookAsync(BookUpdateDto book, int id);
        Task DeleteBookAsync(int id);
    }
}
