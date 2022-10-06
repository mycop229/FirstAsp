namespace Tor.Models.ViewModels
{
    public class DetailsWM
    {
        public DetailsWM()
        {
            Product = new Product();
            Article = new Article();
        }

        public Product Product { get; set; }
        public Article Article { get; set; }

        public bool ExistsInCart { get; set; }

        public int Size { get; set; }
    }
}
