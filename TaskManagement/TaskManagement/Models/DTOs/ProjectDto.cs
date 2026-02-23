namespace TaskManagement.Models.DTOs
{
    public class CreateProjectDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class UpdateProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public CreatorDto CreatedBy { get; set; } = null!;
    }

    public class CreatorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}