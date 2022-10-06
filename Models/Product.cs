using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tor.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Короткое описание")]
        public string ShortDesc { get; set; }

        [Display(Name = "Полное описание")]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Стоимость")]
        public double Price { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "Коллекция")]
        public int ApplicationTypeId { get; set; }
        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType ApplicationType { get; set; }

        [Display(Name = "Артикул")]
        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [Display(Name = "Бренд")]
        public string Brand { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }
    }
   
}
