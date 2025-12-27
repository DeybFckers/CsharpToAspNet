using HotelBookingApi.Contracts.IRepositoies;
using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
        public IActionResult Create([FromBody] Users users)
        {
            try
            {
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
    }
}
