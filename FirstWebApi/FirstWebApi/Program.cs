using FirstWebApi.Contracts;
using FirstWebApi.Repositories;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;

//always do the oop so the code will not redundant for crud operations
var builder = WebApplication.CreateBuilder(args);

var secret = builder.Configuration["MySecretKey"];

//create a token validation parameter then instaniate
var validationParameters = new TokenValidationParameters
{
    ValidateAudience = false,
    ValidateIssuer = false,
    ValidateIssuerSigningKey = true, // you can remove this so it will be lifetime of token
    ValidateLifetime = true, //this is the expireration date of token
    ValidIssuer = string.Empty, //this is just an identifier where the token it came from but mostly it will input the url of the website
    ValidAudience = string.Empty, // it only allows who only use the api endpoint
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
};

builder.Services.AddControllers();
// Register repositories for dependency injection
builder.Services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddSingleton<IHeroRepository, HeroRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITokenService>(o => new TokenServices(validationParameters, secret));
//to enable the authentication add the app.UseAuthentication();
//and app.UseAuthorization(); then install the jwtbearer and authentication
//in nugetpackages and type this
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op =>
    {
        //this is the value to use to validate
        //the token that we provide on request
        //if it is valid or not
        op.TokenValidationParameters = validationParameters;
                                        //instaniate here
    });
//then create a interface of our tokensevice

//swagger ui
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Check if the environment is Development, and use developer exception page if true
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //swagger
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Web Api only");
    });
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
