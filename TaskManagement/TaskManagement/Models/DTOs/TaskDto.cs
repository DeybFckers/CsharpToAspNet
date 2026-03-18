using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public TaskStatusDto Status { get; set; }
        public string? AssignedToUser { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
        [Required]
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
        [Required]
        public TaskStatus Status { get; set; }
    }

    public class AssignTaskDto
    {
        [Required]
        public Guid TaskId { get; set; }
        [Required]
        public Guid UserId { get; set; }

    }
}

