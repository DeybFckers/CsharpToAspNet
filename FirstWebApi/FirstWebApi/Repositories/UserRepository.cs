using FirstWebApi.Contracts;
using FirstWebApi.Infrastructure;
using FirstWebApi.Models;

namespace FirstWebApi.Repositories
{
    /*we just inherit the BaseRepository and then
    *the letter t of BaseReporsitory will be replace as User*/
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /*we use a constructor chaining on base so we can
         * initialize the table of BaseRepository*/
        public UserRepository() : base()
        {
        }
    }
}
