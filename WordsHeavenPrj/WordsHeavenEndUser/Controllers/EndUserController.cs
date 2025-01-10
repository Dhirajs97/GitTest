using Microsoft.AspNetCore.Mvc;
using WordsHeavenEndUser.Interfaces.Services;
using WordsHeavenEndUser.Models;
using WordsHeavenEndUser.Dtos;


namespace WordsHeavenEndUser.Controllers
{
    public class EndUserController : Controller
    {

        private readonly IUserService _userService;

        public EndUserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EndUserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndUserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] EndUserDto userDto)
        {
            await _userService.AddUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] EndUserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }
            await _userService.UpdateUserAsync(userDto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }





    }
}
