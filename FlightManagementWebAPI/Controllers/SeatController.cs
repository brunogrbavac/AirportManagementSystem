using DomainModel.Models;
using FlightManagementWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SeatManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly SeatRepository _seatRepository;
        public SeatController(SeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        [HttpGet]
        public IActionResult GetSeats()
        {
            try
            {
                var seats = _seatRepository.GetSeats(false);
                return Ok(seats);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddSeat([FromBody] Seat seat)
        {
            if (seat == null)
                return BadRequest();

            try
            {
                _seatRepository.InsertSeat(seat);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdateSeat([FromBody] Seat seat)
        {
            if (seat == null)
                return BadRequest();
            try
            {
                _seatRepository.UpdateSeat(seat);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{seatId:int}")]
        public IActionResult GetSeat(int seatId)
        {
            try
            {
                var seat = _seatRepository.GetSeat(seatId);
                return Ok(seat);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete("{seatId:int}")]
        public IActionResult DeleteSeat(int seatId)
        {
            try
            {
                _seatRepository.DeleteSeat(seatId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("checkSeat/{seatId:int}")]
        public IActionResult CheckSeat(int seatId)
        {
            try
            {
                _seatRepository.CheckSeat(seatId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("checkedSeats/{flightId:int}")]
        public IActionResult GetCheckedSeats(int flightId)
        {
            try
            {
                return Ok(_seatRepository.GetCheckedFlightSeats(flightId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("uniqueSeat/{number}/{flightId:int}")]
        public IActionResult UniqueSeat(string number, int flightId)
        {
            try
            {
                return Ok(_seatRepository.UniqueSeatNumber(number, flightId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("flightSeats/{flightId:int}")]
        public IActionResult GetFlightSeats(int flightId)
        {
            try
            {
                return Ok(_seatRepository.GetFlightSeats(flightId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
