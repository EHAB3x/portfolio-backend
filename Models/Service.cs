using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string description { get; set; }
        public string[] features { get; set; }
        public int order { get; set; }
        public string icon { get; set; }
        public string title { get; set; }
    }
}
