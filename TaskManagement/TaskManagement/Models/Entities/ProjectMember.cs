using TaskManagement.Data;

namespace TaskManagement.Models.Entities
{
    public class ProjectMember
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public Guid UserId { get; set; }
        public ApplicationUser user { get; set; } = null!;

        public String Role { get; set; } = "Member";
    }
}
