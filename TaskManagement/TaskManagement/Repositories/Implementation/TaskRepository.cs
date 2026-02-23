using TaskManagement.Data;
using TaskManagement.Models.Entities;
using TaskManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Repositories.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTask(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(Guid id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                throw new KeyNotFoundException("Task not found");
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTask()
        {
            return await _context.TaskItems.ToListAsync();
        }


        public async Task<TaskItem> GetTaskById(Guid id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task UpdateTask(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }
        
    }
}
