using DomainModel.Models;
using FlightManagementWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly CarrierRepository _carrierRepository;
        public CarrierController(CarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        [HttpGet]
        public IActionResult GetCarriers()
        {
            try
            {
                return Ok(_carrierRepository.GetCarriers());
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{carrierId:int}")]
        public IActionResult GetCarrier(int carrierId)
        {
            try
            {
                return Ok(_carrierRepository.GetCarrier(carrierId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddCarrier([FromBody] Carrier carrier)
        {
            if (carrier == null)
                return BadRequest();

            try
            {
                _carrierRepository.InsertCarrier(carrier);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdateCarrier([FromBody] Carrier carrier)
        {
            try
            {
                _carrierRepository.UpdateCarrier(carrier);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{carrierId:int}")]
        public IActionResult DeleteCarrier(int carrierId)
        {
            try
            {
                _carrierRepository.DeleteCarrier(carrierId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
