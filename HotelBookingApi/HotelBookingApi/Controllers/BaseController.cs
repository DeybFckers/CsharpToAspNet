using HotelBookingApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HotelBookingApi.Controllers
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

        [HttpGet("{Id}")]
        public IActionResult GetOne(int Id)
        {
            var user = _repository.GetOne(Id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Create([FromBody] T model)
        {
            try
            {
                _repository.Add(model);

                return Ok(new
                {
                    message = "User Registered Successfull",
                    model = model
                });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public IActionResult Update([FromBody] T model)
        {
            _repository.Update(model);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _repository.Delete(Id);
            return Ok();
        }
        //continue for user controller
    }
}
