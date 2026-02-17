using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolebaseAuthentication.Models;
using RolebaseAuthentication.Repositories.Interface;

namespace RolebaseAuthentication.Controller.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public RoleController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var user = await _userRepository.GetUserByUsername(model.Username);

            if (user == null)
                return NotFound("User not found");

            var result = await _userRepository.AddUserToRole(user, model.Role);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Role assigned");
        }
    }
}
