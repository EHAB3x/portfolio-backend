using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace myapp.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
         private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomePageCard>>> Get()
      {
             var educations = await _context.Educations.ToListAsync();
            var experiences = await _context.Experiences.ToListAsync();
            var projects = await _context.Projects.ToListAsync();
            var services = await _context.Services.ToListAsync();
            var skills = await _context.Skills.ToListAsync();

            var cards = new List<HomePageCard>()
             {
                new HomePageCard
                {
                    Title = "Education",
                    Description = "View my educational background and qualifications",
                    Link = "api/Education",
                    Length = educations.Count()
                },
                new HomePageCard
                {
                    Title = "Experience",
                    Description = "Explore my professional experience and work history",
                    Link = "api/Experience",
                    Length = experiences.Count()
                },
                new HomePageCard
                {
                    Title = "Projects",
                    Description = "Discover the various projects I have worked on",
                    Link = "api/Projects",
                    Length = projects.Count()
                },
                new HomePageCard
                {
                    Title = "Services",
                    Description = "Learn about the services I offer.",
                    Link = "api/Services"
                },
                new HomePageCard
                {
                    Title = "Skills",
                    Description = "See the list of my skills and competencies",
                    Link = "api/Skills"
                    , Length = skills.Count()
                },
            };

            return Ok(cards);
        }
    }
}

