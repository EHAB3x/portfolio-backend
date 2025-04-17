using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {   private static int _nextId = 3;
        private static List<Project> _projects = new List<Project>()
        {
            new Project {Id = 1, category = "Web Development", img = "project1.jpg", order = 1, link = "https://example.com/project1", title = "Portfolio Website" },    
            new Project {Id = 2, category = "Mobile App", img = "project2.png", order = 2, link = "https://example.com/project2", title = "Task Manager App" }

        };

        [HttpGet]
        public ActionResult<IEnumerable<Project>> Get()
        {
            return Ok(_projects.OrderBy(p => p.order).ToList());
        }
        [HttpGet("{id}")]
        public ActionResult<Project> GetById(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Project> Post([FromBody] Project newProject)
        {
            if (newProject == null)
            {
                return BadRequest("Project data is required.");
            }

            newProject.Id = _nextId++;
            _projects.Add(newProject);

            return CreatedAtAction(nameof(GetById), new { id = newProject.Id }, newProject);
        }
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Project updatedProject)
        {
            if (updatedProject == null || id != updatedProject.Id)
            {
                return BadRequest("Project data is invalid.");
            }

            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            project.category = updatedProject.category;
            project.img = updatedProject.img;
            project.link = updatedProject.link;
            project.order = updatedProject.order;
            project.title = updatedProject.title;
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            _projects.Remove(project);
            return NoContent();
        }
    }
}