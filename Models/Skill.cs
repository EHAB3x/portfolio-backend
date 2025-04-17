using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Skill
    {   [Key]
        public int Id {get; set;}
        public string level { get; set; }
        public string name { get; set; }
        public int order { get; set; }
    }
}
