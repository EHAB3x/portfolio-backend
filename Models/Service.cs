using System.ComponentModel.DataAnnotations;

namespace myapp.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string description { get; set; }
        public string[] features { get; set; }
        public string icon { get; set; }
        public string title { get; set; }
    }
}
