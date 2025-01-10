using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WordsHeavenEndUser.Dtos;
using WordsHeavenEndUser.Interfaces.Services;

namespace WordsHeavenEndUser.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromForm] AuthDto authDto)
        {
            var response = await _authService.Login(authDto);
            if (response.ErrorMessage != null)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage);
                return View(authDto);
            }

            // Set the token as a cookie or return it in the response
            HttpContext.Response.Cookies.Append("JwtToken", response.Token);
            return RedirectToAction("Dashboard", "Home", new { area = "EndUser" });
        }


        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] EndUserDto userDto)
        {
            try
            {
                await _authService.RegisterUser(userDto);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(userDto);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Dashboard", "Home", new { area = "EndUser" });
        }

    }
}
