using HotelBookingApi.Contracts.IServices;
using HotelBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationServices _reservationServices;

        public ReservationController(IReservationServices reservationServices)
        {
            _reservationServices = reservationServices;
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var reservation = _reservationServices.GetById(Id);
            if(reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var reservation = _reservationServices.GetAll();
            return Ok(reservation);
        }
        [HttpPost]
        public IActionResult Create(CreateReservationDto reservation)
        {
            try
            {
                _reservationServices.AddReservation(reservation);
                return Ok(new
                {
                    message ="Reservation Created Successfully"
                });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _reservationServices.DeleteReservation(Id);
                return Ok(new
                {
                    message = "Reservation Removed Successfully"
                });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public IActionResult Update(int Id, UpdateReservationDto updateReservation)
        {
            try
            {
                _reservationServices.UpdateReservation(Id, updateReservation);
                return Ok(new
                {
                    message = "Reservation Updated Successfully"
                });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
