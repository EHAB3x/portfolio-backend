using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myapp.Services;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
         private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AuthController(TokenService tokenService, IConfiguration configuration, DataContext context)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AdminModel model)
        {            
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == model.Username && a.Password == model.Password);

            if (admin != null)
            {
                var token = _tokenService.CreateToken(admin.Username, admin.Type);
                return Ok(new { message = "Login successful", token });
            }            
            else
            {
                return Unauthorized(new { message = "Wrong admin data" });
            }
        }
    }

    public class AdminModel
    {
        public string Username { get; set; }
        public string Password { get; set; } 
    }
}