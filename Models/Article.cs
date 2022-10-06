using System.ComponentModel.DataAnnotations;

namespace Tor.Models
{
    public class Article
    {
        [Key]
        [Required]
        public int ArticleId { get; set; }

        [Display(Name = "Артикул")]
        public string ArticleName { get; set; }

        [Display(Name = "XS")]
        public int XS { get; set; }

        [Display(Name = "S")]
        public int S { get; set; }

        [Display(Name = "M")]
        public int M { get; set; }

        [Display(Name = "L")]
        public int L { get; set; }

        [Display(Name = "XL")]
        public int XL { get; set; }

        [Display(Name = "XXL")]
        public int XXL { get; set; }
    }
}
