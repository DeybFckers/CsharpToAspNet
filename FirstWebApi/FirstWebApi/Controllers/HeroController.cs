using FirstWebApi.Contracts;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class HeroController : BaseController<Hero>
    {
        private readonly IHeroRepository _repository; //call the herorepository so we can use the new function
        public HeroController(IHeroRepository repository) : base(repository)
        {
            _repository = repository;
        }
        //then do this to make the function work
        [HttpGet("ByAge/{age}")]//to work the api route will be like this api/hero/byage/{age}
        public IActionResult GetByAge(int age)
        {
            return Ok(_repository.GetByAge(age));
        }
        //then register to DI so it will work
    }
}
