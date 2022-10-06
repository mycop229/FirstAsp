using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tor.Data;
using Tor.Models;
using Tor.Models.ViewModels;
using Tor.Utility;

namespace Tor.Controllers.Cart
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public ProductCartVM ProductCartVM { get; set; }

        public CartController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment; 
        }

        [HttpGet]
        public async Task<ActionResult> CartIndex()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();

            IEnumerable<Models.Product> prodList = await _db.Product.Where(u => prodInCart.Contains(u.Id)).ToListAsync();

            return View(prodList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CartIndex")]
        public IActionResult CartIndexPost()
        {
            
            return RedirectToAction(nameof(Summary));
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Models.Product> prodList = await _db.Product.Where(u => prodInCart.Contains(u.Id)).ToListAsync();

            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == claim.Value),
                ProductList = prodList.ToList()
            };

            return View(ProductUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPostAsync(ProductUserVM productUserVM)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();

            IEnumerable<Models.Product> prodList = await _db.Product.Where(u => prodInCart.Contains(u.Id)).ToListAsync();

            productUserVM.ProductList = prodList.ToList();

            if (productUserVM.Promocode != null)
            {
                return View(await DownscalePromoAsync(productUserVM.Promocode, productUserVM.NewPrice, productUserVM.OldPrice, productUserVM.DiscountAmount, productUserVM.ProductList)); //Изменение промокода
            }

            else
            {
                await SendEmailAsync(productUserVM); //Отправка сообщения

                foreach (var item in shoppingCartList)
                {
                    await DownscaleSizeAsync(item.Size, item.Article);  //Уменьшение остатков размеров
                }

                _db.SaveChanges();
                return RedirectToAction(nameof(InquiryConfirmation));
            }
        }
        public async Task DownscaleSizeAsync(string size, int article) //Изменение кол-во остатков размера
        {
            Article qualitySize = default;

            if (size == "XS")
            {
                qualitySize = await _db.Article.Where(u => u.ArticleId == article).FirstOrDefaultAsync();
                qualitySize.XS = qualitySize.XS - 1;
            }
            if (size == "S")
            {
                qualitySize = await _db.Article.Where(u => u.ArticleId == article).FirstOrDefaultAsync();
                qualitySize.S = qualitySize.S - 1;
            }
            if (size == "M")
            {
                qualitySize = await _db.Article.Where(u => u.ArticleId == article).FirstOrDefaultAsync();
                qualitySize.M = qualitySize.M - 1;
            }
            if (size == "L")
            {
                qualitySize = await _db.Article.Where(u => u.ArticleId == article).FirstOrDefaultAsync();
                qualitySize.L = qualitySize.L - 1;
            }
            if (size == "XL")
            {
                qualitySize = await _db.Article.Where(u => u.ArticleId == article).FirstOrDefaultAsync();
                qualitySize.XL = qualitySize.XL - 1;
            }
            if (size == "XXL")
            {
                qualitySize = await _db.Article.Where(u => u.ArticleId == article).FirstOrDefaultAsync();
                qualitySize.XXL = qualitySize.XXL - 1;
            }
            _db.Article.Update(qualitySize);
        }
        public async Task<ProductUserVM> DownscalePromoAsync(string promo, double newPrice, double oldPrice, double discountAmount, IList<Models.Product> productList)
        {
            // Селект количества промо

            var remainderPromo = await _db.PromoCode.AsNoTracking().Where(u => u.Name == promo).Select(u => u.ValuePromo).SingleOrDefaultAsync();

            if (remainderPromo <= 0)
            {
                //Обработать отсутсвие промокода
                RedirectToAction(nameof(Error));
            }
            if (remainderPromo > 0)
            {
                // Селект типа промокода
                var typePromo = await _db.PromoCode.AsNoTracking().Where(u => u.Name == promo).Select(u => u.TypePromo).SingleOrDefaultAsync();

                //Селект размера промокода

                var dimensionPromo = await _db.PromoCode.AsNoTracking().Where(u => u.Name == promo).Select(u => u.Dimension).SingleOrDefaultAsync();

                if (typePromo == Models.Type.Sum)
                {
                    //Вычисление для вычитания
                    newPrice = oldPrice - dimensionPromo;
                }
                if (typePromo == Models.Type.Percent)
                {
                    //Вычисление для процентов
                    newPrice = oldPrice - (oldPrice / 100 * dimensionPromo);
                }

                discountAmount = oldPrice - newPrice;

                var quantityIncrement = await _db.PromoCode.AsNoTracking().Where(u => u.Name == promo).FirstOrDefaultAsync();

                quantityIncrement.ValuePromo = quantityIncrement.ValuePromo - 1;

                _db.PromoCode.Update(quantityIncrement);

                await _db.SaveChangesAsync();

                ProductUserVM.Promocode = promo;
                ProductUserVM.NewPrice = newPrice;
                ProductUserVM.OldPrice = oldPrice;
                ProductUserVM.DiscountAmount = discountAmount;
                ProductUserVM.ProductList = productList;
            }
            return (ProductUserVM);

        } //Изменение кол-во промокодов
        public async Task SendEmailAsync(ProductUserVM productUserVM) // Send E-mail message
        {
            var PathToTemplate = @"C:\Users\volko\source\repos\Tor\Template\Otpravit.cshtml";
            var subject = "Оформление нового заказа";
            double summa = 0;
            string HtmlBody;

            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                HtmlBody = sr.ReadToEnd();
            }

            StringBuilder productListSB = new();
            StringBuilder productListToDB = new();
            foreach (var prod in productUserVM.ProductList)
            {
                productListToDB.Append(Environment.NewLine + $"{prod.Name}");
                productListSB.Append($"- Наименование: {prod.Name} <span style='font-size:14px;'> (Стоимость: {prod.Price.ToString("c")})</span><br />");
                summa += prod.Price;
            }

            string messageBody = "";

            string Id_user = clsCommon.GetUserId(this.User);

            if (ProductUserVM.NewPrice != 0)
            {
                messageBody = string.Format(HtmlBody,
                productUserVM.ApplicationUser.FullName,
                productUserVM.NewPrice.ToString("c"),
                productUserVM.ApplicationUser.Email,
                productUserVM.ApplicationUser.PhoneNumber,
                productUserVM.ApplicationUser.City,
                productUserVM.ApplicationUser.Address,
                productListSB.ToString());

                Order order = new Order
                {
                    OrderDate = System.DateTime.Today,
                    OrderPrice = productUserVM.NewPrice.ToString(),
                    OrderList = productListToDB.ToString(),
                    OrderAddress = productUserVM.ApplicationUser.Address,
                    UserId = Id_user
                };
                await _db.Order.AddAsync(order);
            }

            else
            {
                messageBody = string.Format(HtmlBody,
                productUserVM.ApplicationUser.FullName,
                productUserVM.OldPrice.ToString("c"),
                productUserVM.ApplicationUser.Email,
                productUserVM.ApplicationUser.PhoneNumber,
                productUserVM.ApplicationUser.City,
                productUserVM.ApplicationUser.Address,
                productListSB.ToString());

                Order order = new Order
                {
                    OrderDate = System.DateTime.Today,
                    OrderPrice = productUserVM.OldPrice.ToString(),
                    OrderList = productListToDB.ToString(),
                    OrderAddress = productUserVM.ApplicationUser.Address,
                    UserId = Id_user
                };
                await _db.Order.AddAsync(order);
            }

            EmailService emailService = new EmailService();

            await emailService.SendEmailAsync(productUserVM.ApplicationUser.Email, subject, messageBody);
        }





        public IActionResult Error()
        {
            return View();
        }

        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public ActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(CartIndex));
        }


    }
}
