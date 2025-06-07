using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using myapp.Models;
using System.Threading.Tasks;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {        
         private readonly DataContext _context;

        public SkillsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> Get()
        {
            return await _context.Skills.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetById(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        [Authorize]
        [HttpPost]
       
        public async Task<ActionResult<Skill>> Post(Skill skill)
        {
            if (skill == null)
            {
                return BadRequest("Skill data is invalid.");
            }
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var skill = _context.Skills.Find(id);
            if (skill is null)
            {
                return NotFound();
            }
            _context.Skills.Remove(skill);
            _context.SaveChanges();
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Skill skill)
        {
             if (id != skill.Id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

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
    }
}