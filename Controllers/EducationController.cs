using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using myapp.Models;
using System.Threading.Tasks;


namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly DataContext _context;

        public EducationController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> Get()
        {
            return await _context.Educations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetById(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            return Ok(education);
        }
        
        [HttpPost]
        public async Task<ActionResult<Education>> Post([FromBody] Education newEducation)
        {
            if (newEducation == null)
            {
                return BadRequest("Education data is required.");
            }

            _context.Educations.Add(newEducation);
             await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = newEducation.Id }, newEducation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Education updatedEducation)
        {
            if (id != updatedEducation.Id)
            {
                return BadRequest();            
            }        
            _context.Entry(updatedEducation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
