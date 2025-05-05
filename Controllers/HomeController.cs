using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace myapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomePageCard>>> GetAllDataCounts()
        {
            var cards = new List<HomePageCard>();

            var dbSetProperties = _context.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var prop in dbSetProperties)
            {
                var entityType = prop.PropertyType.GenericTypeArguments[0];
                var dbSet = prop.GetValue(_context);

                // Get CountAsync method for current entity type
                var countAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m =>
                        m.Name == "CountAsync" &&
                        m.GetParameters().Length == 2 &&
                        m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>));

                var genericCountAsyncMethod = countAsyncMethod?.MakeGenericMethod(entityType);

                if (genericCountAsyncMethod != null)
                {
                    var countTask = (Task)genericCountAsyncMethod.Invoke(null, new object[] { dbSet, CancellationToken.None });
                    await countTask.ConfigureAwait(false);
                    var count = (int)countTask.GetType().GetProperty("Result")?.GetValue(countTask);

                    cards.Add(new HomePageCard
                    {
                        Title = prop.Name,
                        Description = $"This card shows total records in {prop.Name}",
                        Link = $"{prop.Name}",
                        Length = count
                    });
                }
            }

            return Ok(cards);
        }
    }
}
