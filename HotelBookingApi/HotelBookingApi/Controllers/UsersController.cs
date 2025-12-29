using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersServices;
        public UsersController(IUsersService service)
        {
            _usersServices = service;
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var user = _usersServices.GetById(Id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _usersServices.GetAll();
            return Ok(users);
        }
        [HttpPost]
        public IActionResult Create(CreateUsersDto users)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _usersServices.AddUser(users);
                return Ok(new
                {
                    message = "User Registered Successfull",
                    users = users
                });
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public IActionResult Update(int id, UpdateUsersDto users)
        {
            try
            {
                _usersServices.UpdateUser(id, users);
                return Ok(new
                {
                    message = "User Updated Successfully"
                });
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _usersServices.DeleteUser(Id);
                return Ok(new
                {
                    message = "User Remove Successfully"
                });

            }catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
