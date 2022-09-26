using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tor.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Дата заказа")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Сумма заказа")]
        public string OrderPrice { get; set; }

        [DisplayName("Список покупок")]
        public string OrderList { get; set; }

        [DisplayName("Адрес доставки")]
        public string OrderAddress { get; set; }
        public string UserId { get; set; }
    }
}
