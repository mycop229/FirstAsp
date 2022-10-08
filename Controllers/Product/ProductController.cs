using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tor.Data;
using Tor.Models.ViewModels;

namespace Tor.Controllers.Product
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnviroment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnviroment)
        {
            _db = db;
            _webHostEnviroment = webHostEnviroment;
        }

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            IEnumerable<Models.Product> objList = await _db.Product.Include(u=>u.Category).Include(u=>u.ApplicationType).ToListAsync();

            return View(objList);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Models.Product(),
                Article = new Models.Article(),

                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = await _db.Product.FindAsync(id);

                var article = await _db.Article.Where(u => u.ArticleId == productVM.Product.ArticleId).Select(u => u.ArticleId).FirstOrDefaultAsync();

                productVM.Article = await _db.Article.FindAsync(article);

                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnviroment.WebRootPath;

                if(productVM.Product.Id == 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;

                    

                    await _db.Article.AddAsync(productVM.Article);

                    await _db.SaveChangesAsync();

                    int lastId = await _db.Article.OrderBy(u => u.ArticleId).Select(u => u.ArticleId).LastOrDefaultAsync();

                    productVM.Article.ArticleName = lastId.ToString();

                    _db.Article.Update(productVM.Article);

                    productVM.Product.ArticleId = lastId;




                    await _db.Product.AddAsync(productVM.Product);
                    
                }
                else
                {
                    var objFromDb = await _db.Product.AsNoTracking().FirstOrDefaultAsync(u => u.Id == productVM.Product.Id);

                    var articleId = await _db.Article.Where(u => u.ArticleId == objFromDb.ArticleId).AsNoTracking().FirstOrDefaultAsync();

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }

                    productVM.Article.ArticleName = productVM.Article.ArticleId.ToString();

                    _db.Article.Update(productVM.Article);

                    _db.Product.Update(productVM.Product);
                    
                }
                await _db.SaveChangesAsync();

                return RedirectToAction("ProductIndex");

            }

            productVM.CategorySelectList = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            productVM.ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Models.Product product = await _db.Product.Include(u=>u.Category).Include(u=>u.ApplicationType).FirstOrDefaultAsync(u=>u.Id==id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var product = _db.Product.Find(id);

            var articleId = await _db.Product.Where(u => u.Id == product.Id).Select(u => u.ArticleId).FirstOrDefaultAsync();

            var article = await _db.Article.Where(u => u.ArticleId == articleId).FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            string upload = _webHostEnviroment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, product.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }


            _db.Product.Remove(product);
            _db.Article.Remove(article);
            await _db.SaveChangesAsync();
            return RedirectToAction("ProductIndex");
        }
    }
}
