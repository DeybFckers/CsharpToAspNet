using FluentValidation;
using LibraryManagementEFCORE.Models.DTOs;
using LibraryManagementEFCORE.Services.Interfaces;

namespace LibraryManagementEFCORE.Minimalapi
{
    public static class BookMinimalApi
    {
        public static void MapBookEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/api/books").WithTags("Books");

            //Get all books
            group.MapGet("/", async (IBookServices services) =>
            {
                var books = await services.GetAllBookAsync();
                return books is null ? Results.NotFound() : Results.Ok(books);
            });

            //Get books by id
            group.MapGet("/{id}", async (IBookServices services, int id) =>
            {
                var books = await services.GetByBookIdAsync(id);
                return books is null ? Results.NotFound() : Results.Ok(books);
            });

            //Add Books
            group.MapPost("/", async (IBookServices services, BookCreateDto dto, IValidator<BookCreateDto>validator) =>
            {
                var bookValidator = await validator.ValidateAsync(dto);
                if (!bookValidator.IsValid)
                {
                    
                    return Results.BadRequest(bookValidator.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage }));
                }

                await services.AddBookAsync(dto);
                return Results.Ok(new
                {
                    message = "Book Added Succesfully"
                });
            });

            //Update Books
            group.MapPut("/{id}", async (IBookServices services, BookUpdateDto dto, int id) =>
            {
                try
                {
                    var updated = await services.UpdateBookAsync(dto, id);
                    if (!updated)
                    {
                        return Results.NotFound(new { message = "Book not found" });
                    }

                    return Results.Ok(new { message = "Book updated successfully" });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            //Delete Books
            group.MapDelete("/{id}", async (IBookServices services, int id) =>
            {
                await services.DeleteBookAsync(id);
                return Results.Ok(new
                {
                    message = "Book Delete Sucessfully"
                });
            });
        }
    }
}
