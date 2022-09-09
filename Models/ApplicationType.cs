using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tor.Models
{
    public class ApplicationType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Название")]
        public string Name { get; set; } 
    }
}
