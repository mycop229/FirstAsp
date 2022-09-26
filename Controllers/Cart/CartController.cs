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

            ViewBag.Promocode = "AYE";

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
        public async Task<IActionResult> SummaryPostAsync(ProductUserVM ProductUserVM)
        {
                if (ProductUserVM.Promocode != null)
                {

                // Селект количества промо

                var remainderPromo = await _db.PromoCode.AsNoTracking().Where(u => u.Name == ProductUserVM.Promocode).Select(u => u.ValuePromo).SingleOrDefaultAsync(); 

                if(remainderPromo <= 0)
                {
                    //Обработать отсутсвие промокода
                    return RedirectToAction(nameof(Error));
                }
                if(remainderPromo > 0)
                {
                    // Селект типа промокода
                    var typePromo = await _db.PromoCode.AsNoTracking().Where(u => u.Name == ProductUserVM.Promocode).Select(u => u.TypePromo).SingleOrDefaultAsync();

                    //Селект размера промокода

                    var dimensionPromo = await _db.PromoCode.AsNoTracking().Where(u => u.Name == ProductUserVM.Promocode).Select(u => u.Dimension).SingleOrDefaultAsync();

                    if (typePromo == Models.Type.Sum) 
                    {
                        //Вычисление для вычитания
                        ProductUserVM.NewPrice = ProductUserVM.OldPrice - dimensionPromo;
                    }
                    if(typePromo == Models.Type.Percent)
                    {
                        //Вычисление для процентов
                        ProductUserVM.NewPrice = ProductUserVM.OldPrice - (ProductUserVM.OldPrice / 100 * dimensionPromo);
                    }

                    ProductUserVM.DiscountAmount = ProductUserVM.OldPrice - ProductUserVM.NewPrice;

                    var quantityIncrement = await _db.PromoCode.AsNoTracking().Where(u => u.Name == ProductUserVM.Promocode).FirstOrDefaultAsync();

                    quantityIncrement.ValuePromo = quantityIncrement.ValuePromo - 1;

                    _db.PromoCode.Update(quantityIncrement);

                    await _db.SaveChangesAsync();
                }



                return View(ProductUserVM);
                }

                else
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
                foreach (var prod in ProductUserVM.ProductList)
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
                    ProductUserVM.ApplicationUser.FullName,
                    ProductUserVM.NewPrice.ToString("c"),
                    ProductUserVM.ApplicationUser.Email,
                    ProductUserVM.ApplicationUser.PhoneNumber,
                    ProductUserVM.ApplicationUser.City,
                    ProductUserVM.ApplicationUser.Address,
                    productListSB.ToString());

                    Order order = new Order
                    {
                        OrderDate = System.DateTime.Today,
                        OrderPrice = ProductUserVM.NewPrice.ToString(),
                        OrderList = productListToDB.ToString(),
                        OrderAddress = ProductUserVM.ApplicationUser.Address,
                        UserId = Id_user
                    };
                    await _db.Order.AddAsync(order);
                }

                else
                {
                    messageBody = string.Format(HtmlBody,
                    ProductUserVM.ApplicationUser.FullName,
                    ProductUserVM.OldPrice.ToString("c"),
                    ProductUserVM.ApplicationUser.Email,
                    ProductUserVM.ApplicationUser.PhoneNumber,
                    ProductUserVM.ApplicationUser.City,
                    ProductUserVM.ApplicationUser.Address,
                    productListSB.ToString());

                    Order order = new Order
                    {
                        OrderDate = System.DateTime.Today,
                        OrderPrice = ProductUserVM.OldPrice.ToString(),
                        OrderList = productListToDB.ToString(),
                        OrderAddress = ProductUserVM.ApplicationUser.Address,
                        UserId = Id_user
                    };
                    await _db.Order.AddAsync(order);
                }

                


                EmailService emailService = new EmailService();

                await emailService.SendEmailAsync(ProductUserVM.ApplicationUser.Email, subject, messageBody);

                
                _db.SaveChanges();

                return RedirectToAction(nameof(InquiryConfirmation));
            }

                
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
