using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repositories.Interfaces;
using LibraryManagementEFCORE.Repository.Interfaces;
using LibraryManagementEFCORE.Services.Interfaces;
using Mapster;

namespace LibraryManagementEFCORE.Services.Implementation
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository _repo;
        private readonly IAuthorRepository _author;

        public BookServices(IBookRepository repo, IAuthorRepository author)
        {
            _repo = repo;
            _author = author;
        }

        public async Task AddBookAsync(BookCreateDto book)
        {
            var books = book.Adapt<Book>();

            await _repo.AddBookAsync(books);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _repo.GetByBookIdAsync(id);
            if(book == null)
            {
                throw new Exception("The Book Does not Exists");
            }

            await _repo.DeleteBookAsync(id);
        }

        public async Task<IEnumerable<BookProfileDto>> GetAllBookAsync()
        {
            var book = await _repo.GetAllBookAsync();
            var bookDto = book.Adapt<IEnumerable<BookProfileDto>>();

            return bookDto;
        }

        public async Task<BookProfileDto> GetByBookIdAsync(int id)
        {
            var book = await _repo.GetByBookIdAsync(id);
            if (book == null) return null;

            var bookDto = book.Adapt<BookProfileDto>();

            return bookDto;
        }

        public async Task<bool> UpdateBookAsync(BookUpdateDto book, int id)
        {
            var books = await _repo.GetByBookIdAsync(id);
            if(books == null)return false;

            var author = await _author.GetByAuthorIdAsync(book.AuthorId);
            if (author == null)
            {
                throw new Exception("The Author does not exist");
            }

            books.AuthorId = book.AuthorId;
            books.Title = book.Title;
            books.References = book.References;

            await _repo.UpdateBookAsync(books);

            return true;

        }
    }
}
