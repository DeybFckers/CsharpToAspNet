using FluentValidation;
using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Repositories.Implementation;
using LibraryManagementEFCORE.Repository.Interfaces;

namespace LibraryManagementEFCORE.Validator.FluentValidator
{
    internal sealed class BookCreateValidator : AbstractValidator<BookCreateDto>
    {
        private readonly IAuthorRepository _repo;
        public BookCreateValidator(IAuthorRepository repo)
        {
            _repo = repo;

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("authorId is Required")
                .MustAsync(async (authorId, cancellation) =>
                {
                    var author = await _repo.GetByAuthorIdAsync(authorId);
                    return author != null;
                }).WithMessage("Author does not exist");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is Required");

            RuleFor(x => x.References).NotEmpty().WithMessage("References is Required");
        }
    }
}
