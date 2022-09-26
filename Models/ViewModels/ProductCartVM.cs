using System.Collections.Generic;

namespace Tor.Models.ViewModels
{
    public class ProductCartVM
    {
        public ProductCartVM()
        {
            ProductList = new List<Product>();
        }

        public IList<Product> ProductList { get; set; }
    }
}
