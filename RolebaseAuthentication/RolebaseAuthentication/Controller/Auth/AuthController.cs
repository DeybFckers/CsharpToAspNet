using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolebaseAuthentication.Models;
using RolebaseAuthentication.Services.Implementation;
using RolebaseAuthentication.Services.Interface;

namespace RolebaseAuthentication.Controller.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var success = await _authServices.RegisterAsync(model);
            if (!success) return BadRequest("Registration failed");

            return Ok("User registered successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var token = await _authServices.LoginAsync(model);
            if (token == null) return Unauthorized("Invalid credentials");
            return Ok(new { Token = token });
        }
    }
}
