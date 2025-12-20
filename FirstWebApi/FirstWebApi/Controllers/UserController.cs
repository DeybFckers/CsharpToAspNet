using FirstWebApi.Contracts;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    /*inherit from basecontroller and the letter T 
     * change it to model class Name then automatically
     we have crud operations now*/
   public class UserController : BaseController<User>
    {
        //apply a constructor chaining so we can
        //pass the repository from injection
        public UserController(IBaseRepository<User> repository) : base(repository)
        //update the paramater(IBaseRepository) to this IUserRepository repository if you want to add a function
        {
        }
    }
}
