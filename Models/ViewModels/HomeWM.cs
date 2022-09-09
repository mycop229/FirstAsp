using System.Collections.Generic;

namespace Tor.Models.ViewModels
{
    public class HomeWM
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
