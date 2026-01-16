using LibraryManagementEFCORE.Data;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementEFCORE.Repositories.Implementation
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAuthorAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByAuthorIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            await _context.SaveChangesAsync();

        }
    }
}
