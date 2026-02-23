using TaskManagement.Data;

namespace TaskManagement.Models.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public Guid CreatedByUserId { get; set; }
        public ApplicationUser CreatedBy { get; set; } = null!;

        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
