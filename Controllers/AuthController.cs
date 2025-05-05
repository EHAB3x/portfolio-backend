using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myapp.Services;
using myapp.Models;

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
        public async Task<IActionResult> Login([FromBody] Admin model)
        {            
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == model.Username && a.Password == model.Password);

            if (admin != null)
            {
                var token = _tokenService.CreateToken(admin.Username, admin.Password);
                return Ok(new { message = "Login successful", token });
            }            
            else
            {
                return Unauthorized(new { message = "Please enter a vaild username or password" });
            }
        }

        [Authorize]
        [HttpGet("Admins")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        [Authorize]
        [HttpPost("Admins")]
        public async Task<ActionResult<Admin>> AddAdmin([FromBody] Admin admin)
        {
            if (admin == null)
            {
                return BadRequest("Admin data is invalid.");
            }

            var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == admin.Username);

            if (existingAdmin != null)
            {
                return Conflict(new { message = "An admin with the same username already exists." });
            }

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("Admins/{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin is null)
            {
                return NotFound();
            }
            _context.Admins.Remove(admin);
            _context.SaveChanges();
            return NoContent();
        }

    }
}