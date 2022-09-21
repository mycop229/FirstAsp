using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tor.Models
{
    public class MailingList
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("E-mail")]
        [MaxLength(50)]
        public string Email {get;set;}
    }
}
