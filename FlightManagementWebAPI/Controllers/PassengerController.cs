using DomainModel.Models;
using FlightManagementWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerRepository _passengerRepository;
        public PassengerController(PassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        [HttpGet]
        public IActionResult GetPassengers()
        {
            try
            {
                return Ok(_passengerRepository.GetPassengers());
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{passengerId:int}")]
        public IActionResult GetPassenger(int passengerId)
        {
            try
            {
                return Ok(_passengerRepository.GetPassenger(passengerId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddPassenger([FromBody] Passenger passenger)
        {
            if (passenger == null)
                return BadRequest();

            try
            {
                _passengerRepository.InsertPassenger(passenger);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdatePassenger([FromBody] Passenger passenger)
        {
            try
            {
                _passengerRepository.UpdatePassenger(passenger);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{passengerId:int}")]
        public IActionResult DeletePassenger(int passengerId)
        {
            try
            {
                _passengerRepository.DeletePassenger(passengerId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
