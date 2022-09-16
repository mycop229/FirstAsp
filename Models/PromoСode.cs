using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tor.Models
{
    public class PromoСode
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Количество")]
        public int ValuePromo { get; set; }

        [Display(Name = "Тип")]
        public Type TypePromo { get; set; }

        [Display(Name = "Размерность")]
        public int Dimension { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
    }

    public enum Type
    {
        [Display(Name="Вычитаемость")]
        Sum = 1,
        [Display(Name="Процентность")]
        Percent = 2
    }
}
