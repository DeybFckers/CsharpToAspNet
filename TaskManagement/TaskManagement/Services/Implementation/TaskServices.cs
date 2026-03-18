using Mapster;
using TaskManagement.Models.DTOs;
using TaskManagement.Models.Entities;
using TaskManagement.Repositories.Interface;
using TaskManagement.Services.Interface;

namespace TaskManagement.Services.Implementation
{
    public class TaskServices : ITaskServices
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public TaskServices(ITaskRepository taskRepository, IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;

            // Mapster config to map AssignedToUser automatically
            TypeAdapterConfig<TaskItem, TaskDto>.NewConfig()
                .Map(dest => dest.AssignedToUser, src => src.AssignedTo != null
                    ? $"{src.AssignedTo.FirstName} {src.AssignedTo.LastName}"
                    : null);
            
        }

        public async Task<TaskItem> AddTask(CreateTaskDto dto)
        {
            var project = await _projectRepository.GetProjectByIdForFind(dto.ProjectId);
            if (project == null)
                throw new KeyNotFoundException("Project not found");

            var task = dto.Adapt<TaskItem>();
            task.Status = Models.Entities.TaskStatus.Todo;

            await _taskRepository.AddTask(task);
            return task;
        }

        public async Task DeleteTask(Guid id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            await _taskRepository.DeleteTask(id);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTask();
            return tasks.Adapt<IEnumerable<TaskDto>>();
        }

        public async Task<TaskDto> GetTaskById(Guid id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            return task.Adapt<TaskDto>();
        }

        public async Task<TaskDto> UpdateTask(TaskDto dto)
        {
            var task = await _taskRepository.GetTaskById(dto.Id);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            task.Title = dto.Title;
            task.Description = dto.Description;

            // Map DTO enum to entity enum
            task.Status = dto.Status switch
            {
                TaskStatusDto.Todo => Models.Entities.TaskStatus.Todo,
                TaskStatusDto.InProgress => Models.Entities.TaskStatus.InProgress,
                TaskStatusDto.Done => Models.Entities.TaskStatus.Done,
                _ => task.Status
            };

            task.AssignedToUserId = string.IsNullOrEmpty(dto.AssignedToUser) ? null : Guid.Parse(dto.AssignedToUser);
            task.DueDate = dto.DueDate;

            await _taskRepository.UpdateTask(task);

            return task.Adapt<TaskDto>();
        }

        public async Task<TaskDto> AssignTask(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            if(task.AssignedToUserId == user.Id)
                throw new InvalidOperationException("Task is already assigned to this user");

            task.AssignedToUserId = userId;
            await _taskRepository.UpdateTask(task);

            task = await _taskRepository.GetTaskById(taskId);
            return task.Adapt<TaskDto>();
        }

        public async Task<TaskDto> UpdateStatus(Guid taskId, TaskStatusDto status)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            var user = await _userRepository.GetUserById(task.AssignedToUserId ?? Guid.Empty);
            
            if(!user.Id.Equals(Guid.Empty) && user.Id != task.AssignedToUserId)
                throw new InvalidOperationException("Only the assigned user can update the task status");

            task.Status = status switch
            {
                TaskStatusDto.Todo => Models.Entities.TaskStatus.Todo,
                TaskStatusDto.InProgress => Models.Entities.TaskStatus.InProgress,
                TaskStatusDto.Done => Models.Entities.TaskStatus.Done,
                _ => task.Status
            };

            await _taskRepository.UpdateTask(task);

            task = await _taskRepository.GetTaskById(taskId);
            return task.Adapt<TaskDto>();
        }
    }
}