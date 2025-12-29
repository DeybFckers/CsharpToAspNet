using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Infrastructure;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FakeDbContext>();

//register repository and services
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddSingleton<IRoomsRepository, RoomsRepository>();
builder.Services.AddScoped<IRoomsServices, RoomsServices>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking Api");
    });
}

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

//minimal api
app.MapGet("/api/Rooms", (IRoomsServices iroomsServices) =>
{
    var rooms = iroomsServices.GetAll();
    return rooms is null ? Results.NotFound() : Results.Ok();
}).WithTags("Rooms");

app.MapGet("/api/Rooms/{id}", (int id,IRoomsServices iroomsServices) =>
{
    var rooms = iroomsServices.GetById(id);
    return rooms is null ? Results.NotFound() : Results.Ok();
}).WithTags("Rooms");

app.MapPost("/api/Rooms", (CreateRoomsDto createRoomsDto,IRoomsServices iroomsServices) =>
{
   //continue in fluent validation
});

app.Run();
