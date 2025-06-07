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

        [HttpGet]
        public IActionResult Get()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

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
        public async Task<IActionResult> Put(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

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
