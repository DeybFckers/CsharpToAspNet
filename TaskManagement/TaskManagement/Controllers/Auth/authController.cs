using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models.DTOs.Auth;
using TaskManagement.Services.Interface;

namespace TaskManagement.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public authController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var result = await _authServices.Register(register);
            if (!result) return BadRequest("User registration failed.");
            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var token = await _authServices.Login(login);
            if (token == null) return Unauthorized("Invalid email or password.");
            return Ok(new { Token = token });
        }
    }
}
