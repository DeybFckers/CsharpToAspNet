using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models.Entities;
using TaskManagement.Repositories.Interface;

namespace TaskManagement.Repositories.Implementation
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProject(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) throw new KeyNotFoundException("Project not found.");
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects
                                 .Include(p => p.CreatedBy)
                                 .ToListAsync();
        }

        public async Task<Project> GetProjectById(Guid id)
        {
            return await _context.Projects
                                .Include(p => p.Tasks)
                                .Include(p => p.CreatedBy)
                                .FirstOrDefaultAsync(p => p.Id == id);
           
        }

        public async Task<Project> GetProjectByIdForFind(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Project> GetProjectByIdForUpdate(Guid id)
        {
            return await _context.Projects
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}