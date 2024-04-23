using System.ComponentModel.DataAnnotations;

namespace WebSunFlower.Models
{
    public class Authorities
    {
        [Key]
        public int ID_Authorize { get; set; }
        public string? Authorize { get; set; }
    }
}
