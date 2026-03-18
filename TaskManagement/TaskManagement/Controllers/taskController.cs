using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Common;
using TaskManagement.Models.DTOs;
using TaskManagement.Services.Interface;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class taskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;
        public taskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [Authorize(Roles = "Admin,Manager,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                var task = await _taskServices.GetTaskById(id);
                if (task == null) return NotFound(ApiResponse<string>.Failure("Task not found."));
                return Ok(new
                {
                    message = "Task found successfully",
                    task = task
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }
        [Authorize(Roles = "Admin,Manager,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _taskServices.GetAllTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager,User")]
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] CreateTaskDto task)
        {
            try
            {
                var createdTask = await _taskServices.AddTask(task);
                return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] TaskDto task)
        {
            try
            {
                var result = await _taskServices.UpdateTask(task);

                if (result == null) return NotFound(ApiResponse<string>.Failure("Task not found."));

                return Ok(ApiResponse<TaskDto>.SuccessResponse(task, "Task Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("assign-task")]
        public async Task<IActionResult> AssignTask([FromBody] AssignTaskDto task)
        {
            try
            {
                await _taskServices.AssignTask(task.TaskId, task.UserId);

                return Ok(ApiResponse<string>.SuccessResponse(task.ToString(), "Task assigned successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager,User")]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateTaskStauts([FromBody] TaskDto task)
        {
            try
            {
                await _taskServices.UpdateStatus(task.Id, task.Status);
                return Ok(ApiResponse<string>.SuccessResponse(task.ToString(), "Task Status Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                var task = await _taskServices.GetTaskById(id);
                if (task == null) return NotFound(ApiResponse<string>.Failure("Task not found."));

                await _taskServices.DeleteTask(id);
                return Ok(ApiResponse<string>.SuccessResponse(null, "Task Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Failure(ex.Message));
            }
        }
    }
}