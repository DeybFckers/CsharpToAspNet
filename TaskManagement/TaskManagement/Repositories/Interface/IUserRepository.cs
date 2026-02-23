using TaskManagement.Data;

namespace TaskManagement.Repositories.Interface
{
    public interface IUserRepository
    {
        Task <IEnumerable<ApplicationUser>> GetAllUsers();
        Task <ApplicationUser> GetUserById(Guid id);
        Task <IEnumerable<ApplicationUser>> GetUserByRole(string role);

    }
}
