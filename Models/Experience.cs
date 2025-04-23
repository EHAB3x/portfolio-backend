using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        public string country { get; set; }
        public string date { get; set; }
        public string place { get; set; }
        public string title { get; set; }
    }
}
