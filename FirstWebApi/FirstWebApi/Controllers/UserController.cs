using FirstWebApi.Contracts;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    /*inherit from basecontroller and the letter T 
     * change it to model class Name then automatically
     we have crud operations now*/
   public class UserController : BaseController<User>
    {
        private readonly IUserRepository _userRepository;
        //apply a constructor chaining so we can
        //pass the repository from injection
        public UserController(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }
        [AllowAnonymous]// to make the endpoint public
        [HttpPost("Login")] //api/user/login because we need to get the user to login
        public IActionResult UserLogin([FromBody] User user)
        {
            var result = _userRepository.Login(user.Email, user.Password);
            if(result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
