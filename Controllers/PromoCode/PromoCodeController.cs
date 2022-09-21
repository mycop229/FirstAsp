using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tor.Data;
using Tor.Models.ViewModels;

namespace Tor.Controllers.PromoCode
{
    public class PromoCodeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PromoCodeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> PromoCodeIndex()
        {
            IEnumerable<Models.PromoСode> response = await _db.PromoCode.ToListAsync();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            PromoCodeVM promoCodeVM = new PromoCodeVM()
            {
                PromoСode = new Models.PromoСode(),

                PromoCodeSelectList = _db.PromoCode.Select(i => new SelectListItem
                {
                    Text = i.TypePromo.ToString()
                })
            };

            if (id == null)
            {
                return View(promoCodeVM);
            }
            else
            {
                promoCodeVM.PromoСode = await _db.PromoCode.FindAsync(id);

                if (promoCodeVM.PromoСode == null)
                {
                    return NotFound();
                }
                return View(promoCodeVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PromoCodeVM promoCodeVM)
        {
            if (ModelState.IsValid)
            {
                if (promoCodeVM.PromoСode.Id == 0)
                {
                    await _db.PromoCode.AddAsync(promoCodeVM.PromoСode);
                }
                else
                {
                   _db.PromoCode.Update(promoCodeVM.PromoСode);
                }
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PromoCodeIndex");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Models.PromoСode promoСode = await _db.PromoCode.FirstOrDefaultAsync(u => u.Id == id);

            if (promoСode == null)
                return NotFound();

            return View(promoСode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var promoCode = _db.PromoCode.Find(id);

            if (promoCode == null)
                return NotFound();

            _db.PromoCode.Remove(promoCode);
            await _db.SaveChangesAsync();
            return RedirectToAction("PromoCodeIndex");
        }
    }
}