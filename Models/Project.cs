using System.ComponentModel.DataAnnotations;

namespace myapp.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string category { get; set; }
        public string img { get; set; }
        public string link { get; set; }
        public string title { get; set; }
    }
}
