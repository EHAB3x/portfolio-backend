using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Models; // Assuming your models are in the Models namespace

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private static List<Education> _educations = new List<Education>()
        {
            // Initialize with Id instead of order as key
            new Education { Id = 1, country = "USA", date = "2020-2024", order = 1, place = "State University", title = "B.Sc. Computer Science" },
            new Education { Id = 2, country = "USA", date = "2018-2020", order = 2, place = "Community College", title = "Associate of Arts" }
        };
        private static int _nextId = 3; // Keep track of the next available ID

        [HttpGet]
        public ActionResult<IEnumerable<Education>> Get()
        {
            return Ok(_educations.OrderBy(e => e.order).ToList()); // Order by 'order' field
        }

        [HttpGet("{id}")]
        public ActionResult<Education> GetById(int id)
        {
            var education = _educations.FirstOrDefault(e => e.Id == id);
            if (education == null)
            {
                return NotFound();
            }
            return Ok(education);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Education> Post([FromBody] Education newEducation)
        {
            if (newEducation == null)
            {
                return BadRequest("Education data is required.");
            }

            // Assign a new ID
            newEducation.Id = _nextId++;
            _educations.Add(newEducation);

            // Return the created education record with its ID, using GetById route
            return CreatedAtAction(nameof(GetById), new { id = newEducation.Id }, newEducation);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Education updatedEducation)
        {
            if (updatedEducation == null || id != updatedEducation.Id)
            {
                return BadRequest("Education data is invalid.");
            }

            var education = _educations.FirstOrDefault(e => e.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            // Update the existing education record
            education.country = updatedEducation.country;
            education.date = updatedEducation.date;
            education.order = updatedEducation.order;
            education.place = updatedEducation.place;
            education.title = updatedEducation.title;

            return NoContent(); // Indicates success, but no content to return
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var education = _educations.FirstOrDefault(e => e.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            _educations.Remove(education);
            return NoContent(); // Indicates success, but no content to return
        }
    }
}
