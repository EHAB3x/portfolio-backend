using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private static List<Experience> _experiences = new List<Experience>()
        {
            new Experience { Id = 1, country = "USA", date = "2022-Present", place = "Tech Company A", title = "Software Engineer" },
            new Experience { Id = 2, country = "USA", date = "2021-2022", place = "Startup B", title = "Junior Developer" }
        };
        private static int _nextId = 3;

        [HttpGet("{id}")]
        public ActionResult<Experience> GetById(int id)
        {
            var experience = _experiences.FirstOrDefault(e => e.Id == id);
            if (experience == null)
            {
                return NotFound();
            }
            return Ok(experience);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Experience> Post([FromBody] Experience newExperience)
        {
            if (newExperience == null)
            {
                return BadRequest("Experience data is required.");
            }

            newExperience.Id = _nextId++;
            _experiences.Add(newExperience);
            return CreatedAtAction(nameof(GetById), new { id = newExperience.Id }, newExperience);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Experience updatedExperience)
        {
            if (updatedExperience == null || id != updatedExperience.Id)
            {
                return BadRequest("Experience data is invalid.");
            }

            var experience = _experiences.FirstOrDefault(e => e.Id == id);
            if (experience == null)
            {
                return NotFound();
            }

            experience.country = updatedExperience.country;
            experience.date = updatedExperience.date;
            experience.place = updatedExperience.place;
            experience.title = updatedExperience.title;

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var experience = _experiences.FirstOrDefault(e => e.Id == id);
            if (experience == null)
            {
                return NotFound();
            }

            _experiences.Remove(experience);
            return NoContent();
        }
    }
}
