using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using myapp.Models;
using System.Collections.Generic;
using System.Linq;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Get()
        {
            var projects = _context.Projects.ToList();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetById(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Project> Post([FromBody] Project newProject)
        {
            if (newProject == null) 
            { 
                return BadRequest("Project data is required."); 
            }
            _context.Projects.Add(newProject);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = newProject.Id }, newProject);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Project updatedProject)
        {
            if (updatedProject == null || id != updatedProject.Id)
            {
                return BadRequest("Project data is invalid.");
            }

            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

             _context.Entry(project).CurrentValues.SetValues(updatedProject);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
                {
                    var project = _context.Projects.FirstOrDefault(p => p.Id == id);
                    if (project == null)
                    {
                        return NotFound();
                    }
                    _context.Projects.Remove(project);                    
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
        }
    }