using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Tor.Models.ViewModels
{
    public class PromoCodeVM
    {
        public PromoСode PromoСode { get; set; }

        public IEnumerable<SelectListItem> PromoCodeSelectList { get; set; }
    }
}
