using DomainModel.Models;
using FlightManagementWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FlightManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                return Ok(_userRepository.GetUsers());
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{userId:int}")]
        public IActionResult GetUser(int userId)
        {
            try
            {
                return Ok(_userRepository.GetUser(userId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            try
            {
                _userRepository.InsertUser(user);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            try
            {
                _userRepository.UpdateUser(user);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{userId:int}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                _userRepository.DeleteUser(userId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{username}/{password}")]
        public IActionResult LoginUser(string username, string password)
        {
            if (username == null ||password ==null)
                return BadRequest();

            try
            {
                return Ok(_userRepository.LoginUser(username, password));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
