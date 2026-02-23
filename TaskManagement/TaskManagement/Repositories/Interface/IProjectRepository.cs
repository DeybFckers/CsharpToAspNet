using TaskManagement.Models.Entities;

namespace TaskManagement.Repositories.Interface
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(Guid id);
        Task<Project> GetProjectByIdForFind(Guid id);
        Task<Project> GetProjectByIdForUpdate(Guid id); 
        Task AddProject(Project project);
        
        Task UpdateProject(Project project);
        Task DeleteProject(Guid id);
    }
}