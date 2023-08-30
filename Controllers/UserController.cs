using desafio_backend.Models;
using desafio_backend.Models.DTO;
using desafio_backend.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace desafio_backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo) 
        {
            _userRepo = userRepo;
        }

        [HttpPost("create-user")]
        public ActionResult CreateUser([FromBody]User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var user = _userRepo.AddUser(model);

            return StatusCode(201, user);
        }

        [HttpGet("user/{id:int}")]
        public ActionResult GetUser(int id) 
        {
            var user = _userRepo.GetUser(id);

            if (user == null) 
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("user")]
        public ActionResult GetUsers()
        {
            List<UserDTO> users = _userRepo.GetUsers();

            return Ok(users);
        }
    }
}
