using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using myapp.Services;

namespace myapp.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;       
        private readonly IConfiguration _configuration;

        public AuthController(TokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("api/login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username == _configuration["AdminCredentials:Username"] && model.Password == _configuration["AdminCredentials:Password"])
            {
                var token = _tokenService.CreateToken(model.Username);               
                return Ok(new { message = "Login successful", token = token });
            }
            else
            {
                return Unauthorized(new { message = "Wrong user data" });
            }
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}