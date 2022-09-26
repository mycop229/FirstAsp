using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tor.Data;

namespace Tor.Controllers.Orders
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> OrderListIndex(string id)
        {

            List<Models.Order> remainderPromo = null; 
            string Id_user = clsCommon.GetUserId(this.User);

            if (Id_user == id)
            {
                remainderPromo = await _db.Order.AsNoTracking().Where(u => u.UserId == Id_user).ToListAsync();
            }
            else
            {
                return RedirectToAction("Exp");
            }
            
            
            
            



            return View(remainderPromo);
        }
    }
}
