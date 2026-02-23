namespace TaskManagement.Models.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public TaskStatusDto Status { get; set; }
        public string? AssignedToUser { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public enum TaskStatusDto
    {
        Todo = 0,
        InProgress = 1,
        Done = 2
    }

    public class UpdateTaskStatusDto
    {
        public TaskStatus Status { get; set; }
    }

    public class AssignTaskDto
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }

    }
}

