using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Infrastructure;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using HotelBookingApi.Validator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FakeDbContext>();

//register repository and services
builder.Services.AddScoped<UserValidator>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();


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

app.Run();
