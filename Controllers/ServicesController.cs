using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapp.Models;
using System.Linq;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly DataContext _context;

        public ServicesController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var service = _context.Services.Find(id);
            if (service is null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Service updatedService)
        {
            if (updatedService == null || id != updatedService.Id)
            {
                return BadRequest("Service data is invalid.");
            }

            if (_context.Services.Find(id) == null)
            {
                return NotFound();
            }

            _context.Entry(updatedService).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service is null)
            {
                return NotFound();
            }
            _context.Services.Remove(service);
            _context.SaveChanges();
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(Service newService)
        {
            if (newService is null)
            {
                return BadRequest("Service data is required.");
            }
            _context.Services.Add(newService);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = newService.Id }, newService);
        }
    }
}
