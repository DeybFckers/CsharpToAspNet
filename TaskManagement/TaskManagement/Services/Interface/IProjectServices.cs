using TaskManagement.Models.DTOs;

namespace TaskManagement.Services.Interface
{
    public interface IProjectServices
    {
        Task<IEnumerable<ProjectDto>> GetAllProjects();
        Task<ProjectWithTasksDto> GetProjectById(Guid id);
        Task<ProjectDto> AddProject(CreateProjectDto project, Guid userId);
        Task DeleteProject(Guid id);
        Task<bool> UpdateProject(UpdateProjectDto project); // changed from ProjectDto
    }
}