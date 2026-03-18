using Mapster;
using TaskManagement.Models.DTOs;
using TaskManagement.Models.Entities;
using TaskManagement.Repositories.Interface;
using TaskManagement.Services.Interface;

namespace TaskManagement.Services.Implementation
{
    public class ProjectServices : IProjectServices
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectServices(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> AddProject(CreateProjectDto project, Guid userId)
        {
            var newProject = project.Adapt<Project>();
            newProject.CreatedByUserId = userId;
            newProject.CreatedAt = DateTime.UtcNow;

            await _projectRepository.AddProject(newProject);

            var saved = await _projectRepository.GetProjectById(newProject.Id);

            return saved.Adapt<ProjectDto>();   
        }

        public async Task DeleteProject(Guid id)
        {
            var project = await _projectRepository.GetProjectById(id);
            if(project == null)
                throw new Exception("Project not found");

            await _projectRepository.DeleteProject(id);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjects()
        {
            var allProjects = await _projectRepository.GetAllProjects();
            return allProjects.Adapt<IEnumerable<ProjectDto>>();
        }

        public async Task<ProjectWithTasksDto> GetProjectById(Guid id)
        {
            var project = await _projectRepository.GetProjectById(id);
            if (project == null) return null;
            return project.Adapt<ProjectWithTasksDto>();
        }

        public async Task<bool> UpdateProject(UpdateProjectDto project)
        {
            
            var existingProject = await _projectRepository.GetProjectByIdForUpdate(project.Id);
            if (existingProject == null) return false;

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            await _projectRepository.UpdateProject(existingProject);
            return true;
        }
    }
}