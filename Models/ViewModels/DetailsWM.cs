namespace Tor.Models.ViewModels
{
    public class DetailsWM
    {
        public DetailsWM()
        {
            Product = new Product();
        }

        public Product Product { get; set; }
        public bool ExistsInCart { get; set; }
    }
}
