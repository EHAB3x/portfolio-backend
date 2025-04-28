using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using Models;

namespace myapp.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<HomePageCard>> Get()
        {
            var educationController = HttpContext.RequestServices.GetRequiredService<EducationController>();
            var experienceController = HttpContext.RequestServices.GetRequiredService<ExperienceController>();
            var projectsController = HttpContext.RequestServices.GetRequiredService<ProjectsController>();
            var servicesController = HttpContext.RequestServices.GetRequiredService<ServicesController>();
            var skillsController = HttpContext.RequestServices.GetRequiredService<SkillsController>();

            var educations = educationController.Get();
            var experiences = experienceController.Get();
            var projects = projectsController.Get();
            var services = servicesController.Get();
            var skills = skillsController.Get();

            List<Education> educationList = (educations.Result as OkObjectResult)?.Value as List<Education> ?? new List<Education>();
            List<Experience> experienceList = (experiences.Result as OkObjectResult)?.Value as List<Experience> ?? new List<Experience>();
            List<Project> projectList = (projects.Result as OkObjectResult)?.Value as List<Project> ?? new List<Project>();
            List<Service> serviceList = (services.Result as OkObjectResult)?.Value as List<Service> ?? new List<Service>();
            List<Skill> skillList = (skills.Result as OkObjectResult)?.Value as List<Skill> ?? new List<Skill>();




            List<HomePageCard> cards = new List<HomePageCard>()
             {
                new HomePageCard
                {
                    Title = "Education",
                    Description = "View my educational background and qualifications.",
                    Link = "education",
                    Length = educationList.Count()
                },
                new HomePageCard
                {
                    Title = "Experience",
                    Description = "Explore my professional experience and work history.",
                    Link = "experience",
                    Length = experienceList.Count()
                },
                new HomePageCard
                {
                    Title = "Projects",
                    Description = "Discover the various projects I have worked on.",
                    Link = "projects",
                    Length = projectList.Count()
                },
                new HomePageCard
                {
                    Title = "Services",
                    Description = "Learn about the services I offer.",
                    Link = "services",
                    Length = serviceList.Count()
                },
                new HomePageCard
                {
                    Title = "Skills",
                    Description = "See the list of my skills and competencies.",
                    Link = "skills",
                    Length = skillList.Count()
                },
            };

            return Ok(cards);
        }
    }
}
