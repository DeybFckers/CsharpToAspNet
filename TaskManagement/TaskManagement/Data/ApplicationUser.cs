using Microsoft.AspNetCore.Identity;
using TaskManagement.Models.Entities;


namespace TaskManagement.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;


        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    }
}
