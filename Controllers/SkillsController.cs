using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private static List<Skill> _skills = new List<Skill>()
        {
            new Skill { Id=1,level = "Advanced", name = "C#", order = 1 },
            new Skill {Id=2, level = "Intermediate", name = "React", order = 2 },
            new Skill {Id=3, level = "Beginner", name = "Python", order = 3 }
        };
        private static int _nextId = 4;

        [HttpGet]
        public ActionResult<IEnumerable<Skill>> Get()
        {
            return Ok(_skills);
        }

        [HttpPost]
        public ActionResult<Skill> Post(Skill skill)
        {
            if (skill == null)
            {
                return BadRequest("Skill data is invalid.");
            }

            skill.Id = _nextId++;
            _skills.Add(skill);
            return CreatedAtAction(nameof(Get), new { id = skill.Id }, skill);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Skill skill)
        {
            var existingSkill = _skills.FirstOrDefault(s => s.order == id);
            if (existingSkill == null)

            {
                return NotFound();
            }
            existingSkill.name = skill.name;
                existingSkill.level = skill.level;
            return NoContent();
        }
         [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var skill = _skills.FirstOrDefault(e => e.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            _skills.Remove(skill);
            return NoContent();
        }
    }
}
