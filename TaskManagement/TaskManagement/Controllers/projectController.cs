using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Services.Interface;
using System.Security.Claims;
using TaskManagement.Models.DTOs;
using TaskManagement.Common;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectController : ControllerBase
    {
        private readonly IProjectServices _projectServices;
        public projectController(IProjectServices projectServices)
        {
            _projectServices = projectServices;
        }

        [Authorize(Roles = "Admin,Manager,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _projectServices.GetProjectById(id);
            if (project == null) return NotFound(ApiResponse<string>.Failure("Project not found."));
            return Ok(new
            {
                message = "Project found successfully",
                project = project
            });
        }

        [Authorize(Roles = "Admin,Manager,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectServices.GetAllProjects();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] CreateProjectDto project)
        {
            try
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdStr == null)
                    return Unauthorized("User ID not found in token.");

                var userId = Guid.Parse(userIdStr);
                var createdProject = await _projectServices.AddProject(project, userId);
                return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, new
                {
                    message = "Project created successfully",
                    project = createdProject,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto project)
        {
            try
            {
                var result = await _projectServices.UpdateProject(project);
                
                return Ok(new
                {
                    message = "Project updated successfully",
                    project = project,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {
                var project = await _projectServices.GetProjectById(id);
                if (project == null) return NotFound(ApiResponse<string>.Failure("Project not found."));

                await _projectServices.DeleteProject(id);
                return Ok(ApiResponse<string>.SuccessResponse(null, "Project deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }
    }
}