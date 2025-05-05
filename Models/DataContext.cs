using Microsoft.EntityFrameworkCore;

namespace myapp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Experience> Experiences { get; set; }
    }
}