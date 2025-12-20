using FirstWebApi.Contracts;
using FirstWebApi.Models;
using FirstWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<T> : Controller
    {
        private readonly IBaseRepository<T> _repository;
        public BaseController(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]//always put id in curly braces if you want a specific user
        public IActionResult GetOne(int id)
        {
            var user = _repository.GetOne(id);
            if (user == null) //user id is null
            {
                return NotFound();// result not found
            }
            return Ok(user); // return user
        }

        [HttpPost]                  //FromBody means from the body of the request
        public IActionResult Create([FromBody] T user)

        {
            try
            {
                //validator
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _repository.Add(user);

                return Ok(new
                {
                    message = "user Registered Sucessfully",
                    user = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] T user)
        {
            //validator
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Update(user);
            return Ok();
        }
        [HttpDelete("{id}")]//always put id in curly braces if you want a specific user
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
