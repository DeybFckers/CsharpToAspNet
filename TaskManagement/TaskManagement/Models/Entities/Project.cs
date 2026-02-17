using TaskManagement.Data;

namespace TaskManagement.Models.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; } = null!;
        public ICollection
    }
}
