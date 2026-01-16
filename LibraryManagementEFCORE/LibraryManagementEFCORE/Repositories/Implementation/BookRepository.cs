using LibraryManagementEFCORE.Data;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementEFCORE.Repositories.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByBookIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _context.SaveChangesAsync();
        }
    }
}
