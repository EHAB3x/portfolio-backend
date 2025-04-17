using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Models;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private static readonly List<Service> _services = new List<Service>()
        {
            new Service { Id = 1, description = "Building responsive websites.", features = new[] { "Frontend", "Backend" }, order = 1, icon = "web_icon.png", title = "Web Development" },
            new Service { Id = 2, description = "Creating mobile applications.", features = new[] { "iOS", "Android" }, order = 2, icon = "mobile_icon.png", title = "Mobile App Development" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Service>> Get()
        {
            return Ok(_services);
        }

        [HttpGet("{id}")]
        public ActionResult<Service> GetById(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Service updatedService)
        {
            if (updatedService == null || id != updatedService.Id)
            {
                return BadRequest("Service data is invalid.");
            }
            
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            service.description = updatedService.description;
            service.features = updatedService.features;
            service.icon = updatedService.icon;
            service.title = updatedService.title;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null) return NotFound();
            _services.Remove(service);
            return NoContent();
        }
    }
}
