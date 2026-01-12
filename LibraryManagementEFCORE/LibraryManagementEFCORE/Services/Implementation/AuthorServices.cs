using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repository.Interfaces;
using LibraryManagementEFCORE.Services.Interfaces;
using Mapster;

namespace LibraryManagementEFCORE.Services.Implementation
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IAuthorRepository _repo;

        public AuthorServices(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(AuthorCreateDto author)
        {
            var authors = author.Adapt<Author>();
            authors.Password = BCrypt.Net.BCrypt.HashPassword(author.Password);

            await _repo.AddAsync(authors);

        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<AuthorProfileDto>> GetAllAsync()
        {
            var authors = await _repo.GetAllAsync();

            var authorsDto = authors.Adapt<IEnumerable<AuthorProfileDto>>();

            return authorsDto;
        }

        public async Task<AuthorProfileDto> GetByIdAsync(int id)
        {
            var authors = await _repo.GetByIdAsync(id);
            if (authors == null) return null;

            var authorsDto = authors.Adapt<AuthorProfileDto>();

            return authorsDto;
        }

        public async Task<bool> UpdateAsync(AuthorUpdateDto author, int id)
        {
            var authors = await _repo.GetByIdAsync(id);

            if (authors == null) return false;

            authors.Name = author.Name;
            authors.Email = author.Email;

            await _repo.UpdateAsync(authors, id);
            return true;
        }
    }
}
