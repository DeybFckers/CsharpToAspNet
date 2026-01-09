using LibraryManagementEFCORE.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register the database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();

/*if you want to connect the efcore to database do this on appsettings.json
    "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DATABASENAME;Trusted_connection=true;trustServerCertificate=true;"
  }
then register it and after you register
open package manager console and type
add-migration "initial migration" and after it succeed
runt this again on console
Update-Database - it means it will create the database and table base on entity models
 */

//https://www.youtube.com/watch?v=6YIRKBsRWVI continue tomorrow