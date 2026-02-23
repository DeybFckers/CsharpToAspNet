using TaskManagement.Models.Entities;

namespace TaskManagement.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTask();
        Task<TaskItem> GetTaskById(Guid id);
        Task AddTask(TaskItem task);
        Task UpdateTask(TaskItem task);
        Task DeleteTask(Guid id);
       
    }
}
