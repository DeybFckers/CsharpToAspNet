using FirstWebApi.Contracts;
using FirstWebApi.Infrastructure;
using FirstWebApi.Models;

namespace FirstWebApi.Repositories
{
    /*we just inherit the BaseRepository and then
    *the letter t of BaseReporsitory will be replace as User*/
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ITokenService _tokenService;
        /*we use a constructor chaining on base so we can
         * initialize the table of BaseRepository*/
        public UserRepository(ITokenService tokenService) : base()
        {
            _tokenService = tokenService;
        }

        public string Login(string email, string password)
        {
            //compare the email and password using lambda function
            var founduser = _table
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            //if the user is match we will use the tokenserivce and it generate the token
            return founduser != null ? _tokenService.GenerateToken(founduser) : null;
        }
    }
}
