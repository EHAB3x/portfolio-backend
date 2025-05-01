using System.ComponentModel.DataAnnotations;

namespace myapp.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string level { get; set; }
        public string name { get; set; }
    }
}
