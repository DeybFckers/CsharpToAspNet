using FluentValidation;
using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Infrastructure;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FakeDbContext>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

//register repository and services
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddSingleton<IRoomsRepository, RoomsRepository>();
builder.Services.AddScoped<IRoomsServices, RoomsServices>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationServices, ReservationServices>();
builder.Services.AddScoped<ITokenServices, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtOptions!.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),

            ClockSkew = TimeSpan.Zero,
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//minimal api
app.MapGet("/api/Rooms", (IRoomsServices iroomsServices) =>
{
    var rooms = iroomsServices.GetAll();
    return rooms is null ? Results.NotFound() : Results.Ok(rooms);
}).WithTags("Rooms");

app.MapGet("/api/Rooms/{id}", (int id,IRoomsServices iroomsServices) =>
{
    var rooms = iroomsServices.GetById(id);
    return rooms is null ? Results.NotFound() : Results.Ok(rooms);
}).WithTags("Rooms");

app.MapPost("/api/Rooms", (CreateRoomsDto createRoomsDto,IRoomsServices iroomsServices, IValidator<CreateRoomsDto>validator) =>
{
    var validationResult = validator.Validate(createRoomsDto);

    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(errors);
    }

    iroomsServices.AddRooms(createRoomsDto);

    return Results.Ok(new
    {
        message = "Rooms Added Succesfully"
    });
}).WithTags("Rooms");

app.MapPut("/api/Rooms/{id}", (int id, IRoomsServices iroomsServices, UpdateRoomsDto updateRoomsDto) =>
{
   iroomsServices.UpdateRooms(id, updateRoomsDto);

    return Results.Ok(new
    {
        message = "Rooms Updated Successfully"
    });
}).WithTags("Rooms");

app.MapDelete("/api/Rooms/{id}", (int id, IRoomsServices iroomsServices) =>
{
    iroomsServices.DeleteRooms(id);
    return Results.Ok(new
    {
        message = "Rooms Removed Successfully"
    });
}).WithTags("Rooms");

app.Run();
