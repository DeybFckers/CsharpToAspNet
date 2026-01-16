using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagementEFCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServices _authorServices;

        public AuthorController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorProfileDto>> GetByIdAuthor(int id)
        {
            var author = await _authorServices.GetByAuthorIdAsync(id);
            if (author == null) return NotFound();

            return Ok(new
            {
                message = "Author Found Successfully",
                author = author
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorProfileDto>>> GetAllAuthor()
        {
            var authors = await _authorServices.GetAllAuthorAsync();
            return Ok(authors);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor(AuthorCreateDto authorDto)
        {
            await _authorServices.AddAuthorAsync(authorDto);

            return Ok(new
            {
                message = "Author Registered Successfully",
                author = authorDto
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(AuthorUpdateDto authorDto, int id)
        {
            var authors =await _authorServices.UpdateAuthorAsync(authorDto, id);

            if (!authors)
                return NotFound(new { message = $"Author with id {id} not found" });

            return Ok(new { message = "Author Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _authorServices.GetByAuthorIdAsync(id);
            if (author == null) return NotFound(new { message = $"Author with id {id} not found" });

            await _authorServices.DeleteAuthorAsync(id);

            return Ok(new { message = "Author Deleted Successfully" });
        }
    }
}
