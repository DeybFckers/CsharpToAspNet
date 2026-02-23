using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Common;

using TaskManagement.Services.Interface;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public userController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var isManager = User.IsInRole("Manager");
                var users = await _userServices.GetAllUsers(isManager);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userServices.GetUserById(id);

                if (user == null)
                    return NotFound(ApiResponse<string>.Failure("User not found."));

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetUserByRole(string role)
        {
            try
            {
                var users = await _userServices.GetUserByRole(role);
                if (users == null || !users.Any())
                    return NotFound(ApiResponse<string>.Failure("No users found with the specified role."));
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }
    }
}
