using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using myapp.Models;
using System.Threading.Tasks;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {        
         private readonly DataContext _context;

        public ExperienceController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Experience>>> Get()
        {
            return await _context.Experiences.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Experience>> GetById(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
            {
                return NotFound();
            }

            return Ok(experience);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Experience>> Post([FromBody] Experience newExperience)
        {
            if (newExperience == null)
            {
                return BadRequest("Experience data is required.");
            }

            _context.Experiences.Add(newExperience);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = newExperience.Id }, newExperience);
        }

        [Authorize]
        [HttpPut("{id}")]        
        public async Task<IActionResult> Put(int id, [FromBody] Experience updatedExperience)
        {
            if (updatedExperience == null || id != updatedExperience.Id)
            {
                return BadRequest("Experience data is invalid.");
            }
             var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
                return NotFound();

           
            _context.Entry(experience).CurrentValues.SetValues(updatedExperience);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

