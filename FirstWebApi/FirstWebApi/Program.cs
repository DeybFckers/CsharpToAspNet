using FirstWebApi.Contracts;
using FirstWebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

//always do the oop so the code will not redundant for crud operations
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Register repositories for dependency injection
builder.Services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddSingleton<IHeroRepository, HeroRepository>(); 
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
