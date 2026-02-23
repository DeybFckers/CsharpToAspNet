using TaskManagement.Models.DTOs;
using TaskManagement.Models.Entities;

namespace TaskManagement.Services.Interface
{
    public interface ITaskServices
    {
        Task<IEnumerable<TaskDto>> GetAllTasks();
        Task<TaskDto> GetTaskById(Guid id);
        Task<TaskItem> AddTask(CreateTaskDto task);
        Task DeleteTask(Guid id);
        Task<TaskDto> UpdateTask(TaskDto task);
        Task AssignTask(Guid taskId, Guid userId);
        Task UpdateStatus(Guid taskId, TaskStatusDto status);
    }
}
