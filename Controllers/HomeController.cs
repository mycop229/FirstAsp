using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tor.Data;
using Tor.Models;
using Tor.Models.ViewModels;
using Tor.Utility;

namespace Tor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Authorize()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {


            HomeWM homeWM = new()
            {
                Products = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Include(u => u.Article).ToListAsync(),
                Categories = _db.Category,
                Articles = _db.Article
            };



            return View(homeWM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                 && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            DetailsWM detailsVM = new DetailsWM()
            {
                Product = await _db.Product.Include(u => u.Category).Include(u => u.ApplicationType).Include(u => u.Article).Where(u => u.Id == id).FirstOrDefaultAsync(),
                ExistsInCart = false,
            };



            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == id)
                {
                    detailsVM.ExistsInCart = true;
                }

            }

            return View(detailsVM);
        }



        


        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id, string size)
        {
            var selectArticle = _db.Product.AsNoTracking().Where(u => u.Id == id).Select(u => u.ArticleId).SingleOrDefault();

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
           
           if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart)!=null 
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            { 
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList.Add(new ShoppingCart { ProductId = id, Size = size, Article = selectArticle });

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);






          

            

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                 && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            var itemToRemove = shoppingCartList.SingleOrDefault(r => r.ProductId == id);

            
            if(itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
