using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Tor.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }

        public Article Article { get; set; } 
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set; }
        //public IEnumerable<SelectListItem> SizeSelectList { get; set; }
    }
}
