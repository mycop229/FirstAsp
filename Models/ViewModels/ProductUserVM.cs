using System.Collections.Generic;

namespace Tor.Models.ViewModels
{
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList = new List<Product>();
        }

        public ApplicationUser ApplicationUser { get; set; }

        public IList<Product> ProductList { get; set; }

        public double OldPrice { get; set; }

        public double NewPrice { get; set; }

        public double DiscountAmount { get; set; }

        public string Promocode { get; set; }
    }
}
