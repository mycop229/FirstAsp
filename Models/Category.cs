using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        [DisplayName("Display order")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Порядок отображения категории должен быть больше 0")]
        public int DisplayOrder { get; set; }
    }
}
