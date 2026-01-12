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

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task UpdateAsync(Author author, int id)
        {
            var authors = await _context.Authors.FindAsync(id);
            authors.Name = author.Name;
            authors.Email = author.Email;

        }
    }
}
