using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Models.Entities;
using LibraryManagementEFCORE.Repository.Interfaces;
using LibraryManagementEFCORE.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace LibraryManagementEFCORE.Services.Implementation
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IAuthorRepository _repo;

        public AuthorServices(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAuthorAsync(AuthorCreateDto author)
        {
            var authors = author.Adapt<Author>();
            authors.Password = BCrypt.Net.BCrypt.HashPassword(author.Password);

            await _repo.AddAuthorAsync(authors);

        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _repo.DeleteAuthorAsync(id);
        }

        public async Task<IEnumerable<AuthorProfileDto>> GetAllAuthorAsync()
        {
            var authors = await _repo.GetAllAuthorAsync();

            var authorsDto = authors.Adapt<IEnumerable<AuthorProfileDto>>();

            return authorsDto;
        }

        public async Task<AuthorProfileDto> GetByAuthorIdAsync(int id)
        {
            var authors = await _repo.GetByAuthorIdAsync(id);
            if (authors == null) return null;

            var authorsDto = authors.Adapt<AuthorProfileDto>();

            return authorsDto;
        }

        public async Task<bool> UpdateAuthorAsync(AuthorUpdateDto author, int id)
        {
            var authors = await _repo.GetByAuthorIdAsync(id);

            if (authors == null) return false;

            authors.Name = author.Name;
            authors.Email = author.Email;

            await _repo.UpdateAuthorAsync(authors);
            return true;
        }
    }
}
