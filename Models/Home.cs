using System.ComponentModel.DataAnnotations;

namespace myapp.Models
{
    public class HomePageCard
    {
        [Key]
        public int Id {get; set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Length { get; set; }
    }
}