using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tor.Models.ViewModels
{
    public class HomeWM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Article> Articles { get; set; }
        public int SortBy { get; set; }
        

    }
}
