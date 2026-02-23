using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models.DTOs.Auth;
using TaskManagement.Repositories.Interface;

namespace TaskManagement.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class roleController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public roleController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto user)
        {
            var users = await _authRepository.GetUserByEmail(user.Email);
            if (users == null)
            {
                return NotFound("User not found.");
            }

            var result = await _authRepository.AddRolesToUser(users, user.Roles);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);

        }
    }
}
