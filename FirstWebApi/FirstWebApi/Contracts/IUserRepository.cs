using FirstWebApi.Models;

namespace FirstWebApi.Contracts
{
    /*create a repository interface for User model and
      this is where we implement a crud operations*/
    public interface IUserRepository : IBaseRepository<User>
    {
   
    }
}
