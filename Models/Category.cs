using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Название")]
        public string Name { get; set; } 

        [DisplayName("Дисплей ордер")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Порядок отображения категории должен быть больше 0")]
        public int DisplayOrder { get; set; }
    }
}
