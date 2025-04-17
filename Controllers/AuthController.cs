using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using myapp.Services;

namespace myapp.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("api/login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username == "admin" && model.Password == "admin")
            {
                var token = _tokenService.CreateToken(model.Username);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}